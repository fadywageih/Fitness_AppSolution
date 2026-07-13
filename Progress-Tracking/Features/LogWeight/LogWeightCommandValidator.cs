using FluentValidation;

namespace Progress_Tracking.Features.LogWeight
{

    public class LogWeightCommandValidator : AbstractValidator<LogWeightCommand>
    {
        public LogWeightCommandValidator()
        {
            RuleFor(x => x.Weight).InclusiveBetween(40, 200).WithMessage("Weight must be 40-200 kg");
            RuleFor(x => x.Date).NotEmpty();
        }
    }
}
