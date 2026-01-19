using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteBook.API.Data;
using NoteBook.API.DTOs;
using NoteBook.API.Models;
using System.Security.Claims;

namespace NoteBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public PostsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ✅ CREATE POST
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var post = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                UserId = int.Parse(userId)
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return Ok("Post created successfully");
        }

        // ✅ GET LOGGED-IN USER POSTS
        [HttpGet]
        public async Task<IActionResult> GetMyPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var posts = await _context.Posts
                .Where(p => p.UserId == int.Parse(userId))
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return Ok(posts);
        }

        [HttpPost("with-image")]
        public async Task<IActionResult> CreatePostWithImage([FromForm] PostCreateWithImageDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            string? imagePath = null;

            if (dto.Image != null)
            {
                var uploadsFolder = Path.Combine(_env.ContentRootPath, "Uploads", "images");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
                var fullPath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await dto.Image.CopyToAsync(stream);

                imagePath = $"Uploads/images/{fileName}";
            }

            var post = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                ImagePath = imagePath,
                UserId = int.Parse(userId)
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return Ok("Post with image created successfully");
        }
        // ✅ EDIT POST (ONLY OWNER)
        [HttpPut("{id}")]
        public async Task<IActionResult> EditPost(int id, PostUpdateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var post = await _context.Posts.FindAsync(id);

            if (post == null)
                return NotFound("Post not found");

            if (post.UserId != int.Parse(userId))
                return Forbid(); // ❌ Not your post

            post.Title = dto.Title;
            post.Content = dto.Content;

            await _context.SaveChangesAsync();

            return Ok("Post updated successfully");
        }
        // ✅ DELETE POST (ONLY OWNER)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var post = await _context.Posts.FindAsync(id);

            if (post == null)
                return NotFound("Post not found");

            if (post.UserId != int.Parse(userId))
                return Forbid(); // ❌ Not your post

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok("Post deleted successfully");
        }

    }
}
