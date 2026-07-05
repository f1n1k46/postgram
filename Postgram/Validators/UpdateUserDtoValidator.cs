using FluentValidation;
using Postgram.DTOs.User;

namespace Postgram.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator() {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(30)
                .WithMessage("Name cannot exceed 30 characters.")
                .MinimumLength(2)
                .WithMessage("Name must be at least 2 characters.");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .MinimumLength(2)
                .WithMessage("Username must be at least 2 characters.")
                .MaximumLength(30)
                .WithMessage("Username cannot exceed 30 characters.");

            RuleFor(x => x.Nickname)
                .NotEmpty()
                .WithMessage("Nickname is required.")
                .MinimumLength(3)
                .MaximumLength(30);

            RuleFor(x => x.Age)
                .InclusiveBetween(1, 120)
                .WithMessage("Age must be between 1 and 120.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(8)
                .WithMessage("Password must contain at least 8 characters.");
        }
    }
}
