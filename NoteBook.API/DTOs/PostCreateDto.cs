using System.ComponentModel.DataAnnotations;

namespace NoteBook.API.DTOs
{
    public class PostCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
