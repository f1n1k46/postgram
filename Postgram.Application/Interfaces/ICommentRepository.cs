using Postgram.Application.DTOs.CommentDto;
using Postgram.Domain.Models;

namespace Postgram.Application.Interfaces
{
    public interface ICommentRepository
    {
        public Task<Comment> CreateCommentAsync(CreateCommentDto createCommentDto, int userId);
        public Task<Comment> UpdateCommentAsync(int commentId, UpdateCommentDto updateCommentDto);
        public Task<bool> DeleteCommentByIdAsync(int commentId);
        public Task<Comment?> GetCommentByIdAsync(int commentId);
        public Task<List<Comment>> GetAllCommentsAsync();
    }
}
