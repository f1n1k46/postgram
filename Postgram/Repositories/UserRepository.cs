using Microsoft.EntityFrameworkCore;
using Postgram.Data;
using Postgram.DTOs.User;
using Postgram.Models;
using Postgram.Helpers;

namespace Postgram.Repositories
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
    public class UserRepository: IUserRepository
    {
        private readonly PostgramDbContext _context;
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();

        public UserRepository(PostgramDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(CreateUserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == dto.Email ||
                u.Nickname == dto.Nickname);

            if (user != null)
            {
                return user;
            }

            var newUser = new User
            {
                Name = dto.Name,
                Username = dto.Username,
                Nickname = dto.Nickname,
                Age = dto.Age,
                Email = dto.Email,
                PasswordHash = dto.Password
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User?> UpdateUserAsync(int userId, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return null;
            }

            user.Name = dto.Name;
            user.Username = dto.Username;
            user.Nickname = dto.Nickname;
            user.Age = dto.Age;
            user.PasswordHash = _passwordHasher.HashPassword(dto.Password);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        { 
            return await _context.Users.FindAsync(userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<Post>> GetUserPostsAsync(int userId)
        {
            return await _context.Posts
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
}
}
