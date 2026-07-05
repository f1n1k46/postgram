using FluentValidation;
using Postgram.DTOs.CommentDto;

namespace Postgram.Validators
{
    public class UpdateCommentDtoValidator : AbstractValidator<UpdateCommentDto>
    {
        public UpdateCommentDtoValidator() {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Text is required.")
                .MaximumLength(500)
                .WithMessage("Text cannot exceed 500 characters.");
        }
    }
}
