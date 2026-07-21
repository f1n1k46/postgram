using Postgram.Application.DTOs.User;
using Postgram.Domain.Models;

namespace Postgram.Application.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> CreateUserAsync(CreateUserDto dto);
        public Task<bool> DeleteUserAsync(int userId);
        public Task<User?> UpdateUserAsync(int userId, UpdateUserDto dto);
        public Task<User?> GetUserByIdAsync(int userId);
        public Task<List<User>> GetAllUsersAsync();
        public Task<List<Post>> GetUserPostsAsync(int userId);
        public Task<User?> GetUserByEmailAsync(string email);
    }
}
