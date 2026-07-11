using FluentValidation;

namespace IdentityService.Features.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.ResetToken)
                .NotEmpty().WithMessage("Reset token is required");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least 1 uppercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least 1 number");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required");

            RuleFor(x => x)
                .Must(x => x.NewPassword == x.ConfirmPassword)
                .WithMessage("New password and confirmation do not match");
        }
    }
}
