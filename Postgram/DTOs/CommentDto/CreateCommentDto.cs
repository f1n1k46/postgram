namespace Postgram.DTOs.CommentDto
{
    public class CreateCommentDto
    {
        public string Text { get; set; } = "";
        public int PostId { get; set; }
    }
}
