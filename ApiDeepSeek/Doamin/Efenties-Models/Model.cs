using System.ComponentModel.DataAnnotations;

namespace ApiDeepSeek.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        
        public string Name { get; set; }    
        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}