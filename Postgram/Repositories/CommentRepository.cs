using Microsoft.EntityFrameworkCore;
using Postgram.Data;
using Postgram.DTOs.CommentDto;
using Postgram.Models;

namespace Postgram.Repositories
{
    public interface ICommentRepository
    {
        public Task<Comment> CreateCommentAsync(CreateCommentDto createCommentDto, int userId);
        public Task<Comment> UpdateCommentAsync(int commentId, UpdateCommentDto updateCommentDto);
        public Task<bool> DeleteCommentByIdAsync(int commentId);
        public Task<Comment?> GetCommentByIdAsync(int commentId);
        public Task<List<Comment>> GetAllCommentsAsync();
    }
    public class CommentRepository : ICommentRepository
    {
        private readonly PostgramDbContext _context;

        public CommentRepository(PostgramDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateCommentAsync(CreateCommentDto createCommentDto, int userId)
        {
            var comment = new Comment
            {
                Text = createCommentDto.Text,
                UserId = userId,
                PostId = createCommentDto.PostId,
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            
            return comment;
        }

        public async Task<Comment> UpdateCommentAsync(int commentId, UpdateCommentDto updateCommentDto)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                throw new KeyNotFoundException();
            }
            
            comment.Text = updateCommentDto.Text;
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<bool> DeleteCommentByIdAsync(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Comment?> GetCommentByIdAsync(int commentId)
        {
            return await _context.Comments.FindAsync(commentId);
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }
    }
}
