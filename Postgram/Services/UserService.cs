using Postgram.DTOs.PostDto;
using Postgram.DTOs.User;
using Postgram.Repositories;
using FluentValidation;

namespace Postgram.Services
{
    public interface IUserService
    {
        public Task<bool> DeleteUserAsync(int userId);
        public Task<UserResponseDto?> UpdateUserAsync(int userId, UpdateUserDto dto);
        public Task<UserResponseDto?> GetUserByIdAsync(int userId);
        public Task<List<UserResponseDto>> GetAllUsersAsync();
        public Task<List<PostResponseDto>> GetUserPostsAsync(int userId);

    }

    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UpdateUserDto> _updateValidator;

        public UserService(IUserRepository userRepository, IValidator<UpdateUserDto> updateValidator)
        {
            _userRepository = userRepository;
            _updateValidator = updateValidator;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }

        public async Task<UserResponseDto?> UpdateUserAsync(int userId, UpdateUserDto dto)
        {
            var result = await _updateValidator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var user = await _userRepository.UpdateUserAsync(userId, dto);
            
            if (user == null)
            {
                return null;
            }
            
            return new UserResponseDto
            {
                Id = user.UserId,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Nickname = user.Nickname,
                Age = user.Age,
                CreatedAt = user.CreatedAt,
            };
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int userId) 
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            return new UserResponseDto
            {
                Id = userId,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Nickname = user.Nickname,
                Age = user.Age,
                CreatedAt = user.CreatedAt,
            };
        }

        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return users.Select(x => new UserResponseDto
            {
                Id = x.UserId,
                Name = x.Name,
                Username = x.Username,
                Email = x.Email,
                Nickname = x.Nickname,
                Age = x.Age,
                CreatedAt = x.CreatedAt,
            }).ToList();
        }

        public async Task<List<PostResponseDto>> GetUserPostsAsync(int userId)
        {
            var posts = await _userRepository.GetUserPostsAsync(userId);
            return posts.Select(x => new PostResponseDto
            {
                PostId = x.PostId,
                Title = x.Title,
                Description = x.Description,
            }).ToList();
        }
    }
}
 