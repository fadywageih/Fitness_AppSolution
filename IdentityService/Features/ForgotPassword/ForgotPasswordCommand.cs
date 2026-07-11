using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.ForgotPassword
{
    public record ForgotPasswordCommand(string Email) : IRequest<AuthResponse>;

}
