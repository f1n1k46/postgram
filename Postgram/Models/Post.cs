using System.ComponentModel.DataAnnotations;

namespace Postgram.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<Like> Likes { get; set; } = [];
    }
}
