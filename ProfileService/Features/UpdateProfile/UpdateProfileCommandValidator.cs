using FluentValidation;

namespace ProfileService.Features.UpdateProfile
{

    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().Length(2, 50);
            RuleFor(x => x.LastName).NotEmpty().Length(2, 50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^(01)[0-9]{9}$");
        }
    }
}
