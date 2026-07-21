namespace Postgram.Application.DTOs.PostDto
{
    public class PostResponseDto
    {
        public int PostId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
