using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NoteBook.API.DTOs
{
    public class PostCreateWithImageDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }
    }
}
