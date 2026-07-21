using FluentValidation;
using Postgram.Application.DTOs.PostDto;

namespace Postgram.Application.Validators
{
    public class UpdatePostDtoValidator : AbstractValidator<UpdatePostDto>
    {
        public UpdatePostDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(100)
                .WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Text is required.")
                .MaximumLength(1000)
                .WithMessage("Text cannot exceed 1000 characters.");
        }
    }
}
