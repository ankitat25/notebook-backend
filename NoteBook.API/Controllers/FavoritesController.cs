using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteBook.API.Data;
using NoteBook.API.Models;
using System.Security.Claims;

namespace NoteBook.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FavoritesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ❤️ ADD / REMOVE FAVORITE
        [HttpPost("{postId}")]
        public async Task<IActionResult> ToggleFavorite(int postId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var existing = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.PostId == postId);

            if (existing != null)
            {
                _context.Favorites.Remove(existing);
                await _context.SaveChangesAsync();
                return Ok("Removed from favorites");
            }

            var favorite = new Favorite
            {
                UserId = userId,
                PostId = postId
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return Ok("Added to favorites");
        }

        // ❤️ GET MY FAVORITES
        [HttpGet]
        public async Task<IActionResult> GetMyFavorites()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var posts = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Post)
                .Select(f => f.Post)
                .ToListAsync();

            return Ok(posts);
        }
    }
}
