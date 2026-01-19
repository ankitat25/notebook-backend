using System.ComponentModel.DataAnnotations;

namespace NoteBook.API.DTOs
{
    public class PostUpdateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
