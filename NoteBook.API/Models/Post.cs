using System.ComponentModel.DataAnnotations;

namespace NoteBook.API.Models
{
    public class Post
    {
        public  int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        public string? ImagePath { get; set; }   // ✅ THIS LINE


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
        public User User { get; set; }


    }
}
