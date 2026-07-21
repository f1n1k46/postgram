using FluentValidation;
using Postgram.Application.DTOs.CommentDto;

namespace Postgram.Application.Validators
{
    public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentDtoValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Text is required.")
                .MaximumLength(500)
                .WithMessage("Text cannot exceed 500 characters.");

            RuleFor(x => x.PostId)
                .GreaterThan(0)
                .WithMessage("PostId must be greater than 0.");
        }
    }
}