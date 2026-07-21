using Postgram.Application.DTOs.PostDto;
using Postgram.Domain.Models;

namespace Postgram.Application.Interfaces
{
    public interface IPostRepository
    {
        public Task<Post> CreatePostAsync(CreatePostDto dto, int userId);
        public Task<bool> DeletePostAsync(int postId);
        public Task<Post> UpdatePostAsync(int postId, UpdatePostDto dto);
        public Task<Post?> GetPostByIdAsync(int postId);
        public Task<List<Post>> GetAllPostsAsync();
    }
}
