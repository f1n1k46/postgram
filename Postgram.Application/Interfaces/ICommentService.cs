using Postgram.Application.DTOs.CommentDto;

namespace Postgram.Application.Interfaces
{
    public interface ICommentService
    {
        public Task<CommentResponseDto> CreateCommentAsync(CreateCommentDto dto, int userId);
        public Task<CommentResponseDto> UpdateCommentAsync(int commentId, UpdateCommentDto dto);
        public Task<bool> DeleteCommentByIdAsync(int commentId);
        public Task<CommentResponseDto> GetCommentByIdAsync(int commentId);
        public Task<List<CommentResponseDto>> GetAllCommentsAsync();
    }
}
