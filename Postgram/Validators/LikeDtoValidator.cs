using FluentValidation;
using Postgram.DTOs.LikeDto;

namespace Postgram.Validators
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
