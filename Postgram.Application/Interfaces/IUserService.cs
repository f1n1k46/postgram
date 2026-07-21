using Postgram.Application.DTOs.PostDto;
using Postgram.Application.DTOs.User;

namespace Postgram.Application.Interfaces
{
    public interface IUserService
    {
        public Task<bool> DeleteUserAsync(int userId);
        public Task<UserResponseDto?> UpdateUserAsync(int userId, UpdateUserDto dto);
        public Task<UserResponseDto?> GetUserByIdAsync(int userId);
        public Task<List<UserResponseDto>> GetAllUsersAsync();
        public Task<List<PostResponseDto>> GetUserPostsAsync(int userId);

    }
}
