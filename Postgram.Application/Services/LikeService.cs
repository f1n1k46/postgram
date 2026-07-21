using Postgram.Application.DTOs.LikeDto;
using Postgram.Application.Interfaces;
using FluentValidation;

namespace Postgram.Application.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IValidator<LikeDto> _likeValidator;

        public LikeService(ILikeRepository likeRepository, IUserRepository userRepository, IPostRepository postRepository, IValidator<LikeDto> likeValidator)
        {
            _likeRepository = likeRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _likeValidator = likeValidator;
        }

        public async Task<bool> LikePostAsync(LikeDto dto, int userId)
        {
            var result = await _likeValidator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            if (!await UserExistsAsync(userId))
            {
                throw new KeyNotFoundException($"User {userId} not found.");
            }

            if (!await PostExistsAsync(dto.PostId))
            {
                throw new KeyNotFoundException($"Post {dto.PostId} not found.");
            }

            return await _likeRepository.LikePostAsync(dto, userId);
        }

        public async Task<bool> UnlikePostAsync(LikeDto dto, int userId)
        {
            var result = await _likeValidator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            if (!await UserExistsAsync(userId))
            {
                throw new KeyNotFoundException($"User {userId} not found.");
            }

            if (!await PostExistsAsync(dto.PostId))
            {
                throw new KeyNotFoundException($"Post {dto.PostId} not found.");
            }

            return await _likeRepository.UnlikePostAsync(dto, userId);
        }
        private async Task<bool> UserExistsAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId) != null;
        }

        private async Task<bool> PostExistsAsync(int postId)
        {
            return await _postRepository.GetPostByIdAsync(postId) != null;
        }
    }
}
