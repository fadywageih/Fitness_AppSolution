using FluentValidation;

namespace FitnessEngine.Features.SubmitStats
{
    public class SubmitStatsCommandValidator : AbstractValidator<SubmitStatsCommand>
    {
        public SubmitStatsCommandValidator()
        {
            RuleFor(x => x.Weight).InclusiveBetween(40, 200).WithMessage("Weight must be 40-200 kg");
            RuleFor(x => x.Height).InclusiveBetween(140, 220).WithMessage("Height must be 140-220 cm");
            RuleFor(x => x.Age).InclusiveBetween(16, 100).WithMessage("Age must be 16-100");
            RuleFor(x => x.Gender).Must(g => g is "Male" or "Female").WithMessage("Gender must be Male or Female");
            RuleFor(x => x.Goal).InclusiveBetween(1, 5);
            RuleFor(x => x.ActivityLevel).InclusiveBetween(1, 5);
        }
    }
}
