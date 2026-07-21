namespace Postgram.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = "";
        public string Username { get; set; } = "";
        public string Nickname { get; set; } = "";
        public int Age { get; set; }
        public string Email { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public string PasswordHash { get; set; } = "";

        public ICollection<Post> Posts { get; set; } = [];
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<Like> Likes { get; set; } = [];
    }
}
