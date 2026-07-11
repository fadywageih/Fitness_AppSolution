using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.Login
{
    public record LoginCommand(
        string Email,
        string Password
    ) : IRequest<AuthResponse>;

    public record LoginResponse(
        string Token,
        string RefreshToken,
        bool ProfileCompleted,
        bool IsPremium
    );
}
