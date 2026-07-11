using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.Logout
{
    public record LogoutCommand : IRequest<AuthResponse>;

}
