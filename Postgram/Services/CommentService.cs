using Postgram.DTOs.CommentDto;
using Postgram.Repositories;
using FluentValidation;

namespace Postgram.Services
{
    public interface ICommentService
    {
        public Task<CommentResponseDto> CreateCommentAsync(CreateCommentDto dto, int userId);
        public Task<CommentResponseDto> UpdateCommentAsync(int commentId, UpdateCommentDto dto);
        public Task<bool> DeleteCommentByIdAsync(int commentId);
        public Task<CommentResponseDto> GetCommentByIdAsync(int commentId);
        public Task<List<CommentResponseDto>> GetAllCommentsAsync();
    }
    public class CommentService: ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IValidator<CreateCommentDto> _createValidator;
        private readonly IValidator<UpdateCommentDto> _updateValidator;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IPostRepository postRepository,
            IValidator<CreateCommentDto> createValidator, IValidator<UpdateCommentDto> updateValidator)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<CommentResponseDto> CreateCommentAsync(CreateCommentDto dto, int userId)
        {
            var result = await _createValidator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            if (!await UserExistsAsync(userId))
            {
                throw new KeyNotFoundException("User not found.");
            }

            if (!await PostExistsAsync(dto.PostId))
            {
                throw new KeyNotFoundException("Post not found.");
            }

            var comment = await _commentRepository.CreateCommentAsync(dto, userId);
            return new CommentResponseDto
            {
                CommentId = comment.CommentId,
                Text = comment.Text,
            };
        }

        public async Task<CommentResponseDto> UpdateCommentAsync(int commentId, UpdateCommentDto dto)
        {
            var result = await _updateValidator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var comment = await _commentRepository.UpdateCommentAsync(commentId, dto);
            
            return new CommentResponseDto
            {
                CommentId = comment.CommentId,
                Text = comment.Text,
            };
        }

        public async Task<bool> DeleteCommentByIdAsync(int commentId)
        {
            return await _commentRepository.DeleteCommentByIdAsync(commentId);
        }

        public async Task<CommentResponseDto> GetCommentByIdAsync(int commentId)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);

            if (comment == null)
            {
                throw new KeyNotFoundException($"Comment with ID {commentId} not found.");
            }

            return new CommentResponseDto
            {
                CommentId = comment.CommentId,
                Text = comment.Text,
            };
        }

        public async Task<List<CommentResponseDto>> GetAllCommentsAsync()
        {
            var comment = await _commentRepository.GetAllCommentsAsync();
            return comment.Select(c => new CommentResponseDto
            {
                CommentId = c.CommentId,
                Text = c.Text,
            }).ToList();
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
