namespace Postgram.Application.DTOs.User
{
    public class UserResponseDto
    {   
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Username { get; set; } = "";
        public string Nickname { get; set; } = "";
        public int Age { get; set; }
        public string Email { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
