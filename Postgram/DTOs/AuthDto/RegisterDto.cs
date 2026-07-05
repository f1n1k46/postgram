namespace Postgram.DTOs.AuthDto
{
    public class RegisterDto
    {
        public string Name { get; set; } = "";
        public string Username { get; set; } = "";
        public string Nickname { get; set; } = "";
        public int Age { get; set; }
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
