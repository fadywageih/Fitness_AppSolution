using FluentValidation;

namespace IdentityService.Features.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty();
            RuleFor(x => x.NewPassword)
                .NotEmpty().MinimumLength(6)
                .Matches("[A-Z]").WithMessage("Password must contain at least 1 uppercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least 1 number");
            RuleFor(x => x.ConfirmPassword).NotEmpty();
            RuleFor(x => x).Must(x => x.NewPassword == x.ConfirmPassword)
                .WithMessage("Passwords do not match");
        }
    }
}
