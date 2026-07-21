using FluentValidation;
using Postgram.Application.DTOs.LikeDto;

namespace Postgram.Application.Validators
{
    public class LikeDtoValidator : AbstractValidator<LikeDto>
    {
        public LikeDtoValidator() {
            RuleFor(x => x.PostId)
                .GreaterThan(0)
                .WithMessage("PostId must be greater than 0.");
        }
    }
}
