using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.ChangePassword
{
    public record ChangePasswordCommand(
        string CurrentPassword,
        string NewPassword,
        string ConfirmPassword
    ) : IRequest<AuthResponse>;
}
