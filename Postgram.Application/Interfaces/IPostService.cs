using Postgram.Application.DTOs.PostDto;

namespace Postgram.Application.Interfaces
{
    public interface IPostService
    {
        public Task<PostResponseDto> CreatePostAsync(CreatePostDto dto, int userId);
        public Task<bool> DeletePostByIdAsync(int postId);
        public Task<PostResponseDto> UpdatePostAsync(int postId, UpdatePostDto dto);
        public Task<PostResponseDto> GetPostByIdAsync(int postId);
        public Task<List<PostResponseDto>> GetAllPostsAsync();
    }
}
