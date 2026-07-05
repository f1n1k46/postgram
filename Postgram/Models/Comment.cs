using System.ComponentModel.DataAnnotations;

namespace Postgram.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public User User { get; set; } = null!;
        public Post Post { get; set; } = null!;
        public ICollection<Like> Likes { get; set; } = [];
    }
}
