using FluentValidation;
using Postgram.Application.DTOs.PostDto;
using Postgram.Application.Interfaces;

namespace Postgram.Application.Services
{
    public class PostService: IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreatePostDto> _createValidator;
        private readonly IValidator<UpdatePostDto> _updateValidator;

        public PostService(IPostRepository postRepositories, IUserRepository userRepository, IValidator<CreatePostDto> createValidator, IValidator<UpdatePostDto> updateValidator)
        {
            _postRepository = postRepositories;
            _userRepository = userRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<PostResponseDto> CreatePostAsync(CreatePostDto dto, int userId)
        {
            var result = await _createValidator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            if (!await UserExistsAsync(userId))
            {
                throw new ArgumentException($"Invalid user {userId}.");
            }
            
            var post = await _postRepository.CreatePostAsync(dto, userId);
            
            return new PostResponseDto
            {
                PostId = post.PostId,
                Description = post.Description,
                Title = post.Title,
            };
        }

        public async Task<bool> DeletePostByIdAsync(int postId)
        {
            return await _postRepository.DeletePostAsync(postId);
        }

        public async Task<PostResponseDto> UpdatePostAsync(int postId, UpdatePostDto dto)
        {
            var result = await _updateValidator.ValidateAsync(dto);

            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            var post = await _postRepository.UpdatePostAsync(postId, dto);
            return new PostResponseDto
            {
                PostId = post.PostId,
                Description = post.Description,
                Title = post.Title,
            };
        }

        public async Task<PostResponseDto> GetPostByIdAsync(int postId)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);

            if (post == null)
            {
                throw new KeyNotFoundException($"Post with ID {postId} not found.");
            }

            return new PostResponseDto
            {
                PostId = post.PostId,
                Description = post.Description,
                Title = post.Title,
            };
        }

        public async Task<List<PostResponseDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return posts.Select(p => new PostResponseDto
            {
                PostId = p.PostId,
                Description = p.Description,
                Title = p.Title,
            }).ToList();
        }

        private async Task<bool> UserExistsAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId) != null;
        }
    }
}
