using FluentValidation;
using Postgram.Application.DTOs.AuthDto;

namespace Postgram.Application.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(2)
                .WithMessage("Name must be at least 2 characters.")
                .MaximumLength(50)
                .WithMessage("Name cannot exceed 50 characters.");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .MinimumLength(3)
                .MaximumLength(30);

            RuleFor(x => x.Nickname)
                .NotEmpty()
                .WithMessage("Nickname is required.")
                .MinimumLength(3)
                .MaximumLength(30);

            RuleFor(x => x.Age)
                .InclusiveBetween(1, 120)
                .WithMessage("Age must be between 1 and 120.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(8)
                .WithMessage("Password must contain at least 8 characters.");
        }
    }
}
