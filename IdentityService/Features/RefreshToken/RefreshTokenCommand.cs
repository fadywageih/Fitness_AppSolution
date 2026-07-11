using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.RefreshToken
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<AuthResponse>;

}
