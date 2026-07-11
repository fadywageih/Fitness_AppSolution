using MediatR;

namespace ProfileService.Features.UpdateProfile
{
    public record UpdateProfileCommand(
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber
    ) : IRequest<UpdateProfileResponse?>;

    public record UpdateProfileResponse(
        Guid UserId,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber
    );
}
