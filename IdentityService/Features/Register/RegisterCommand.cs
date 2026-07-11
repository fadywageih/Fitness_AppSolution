using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string PhoneNumber
    ) : IRequest<AuthResponse>;

    public record RegisterResponse(
        Guid UserId,
        bool RequiresProfileCompletion
    );

}
