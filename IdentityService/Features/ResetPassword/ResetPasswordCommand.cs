using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.ResetPassword
{
    public record ResetPasswordCommand(
        string ResetToken,
        string NewPassword,
        string ConfirmPassword
    ) : IRequest<AuthResponse>;
}
