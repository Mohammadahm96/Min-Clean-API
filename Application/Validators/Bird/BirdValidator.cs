using FluentValidation;
using Application.Dtos;

namespace Application.Validators.Bird
{
    public class BirdValidator : AbstractValidator<BirdDto>
    {
        public BirdValidator()
        {
            RuleFor(bird => bird.Name)
                .NotEmpty().WithMessage("Bird Name cannot be empty.")
                .NotNull().WithMessage("Bird Name cannot be null.");
        }
    }
}
