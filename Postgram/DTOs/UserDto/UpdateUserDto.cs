namespace Postgram.DTOs.User
{
    public class UpdateUserDto
    {
        public string Name { get; set; } = "";
        public string Username { get; set; } = "";
        public string Nickname { get; set; } = "";
        public int Age { get; set; }
        public string Password { get; set; } = "";
    }
}