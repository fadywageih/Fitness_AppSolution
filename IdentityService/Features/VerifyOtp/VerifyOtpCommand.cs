using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.VerifyOtp
{
    public record VerifyOtpCommand(string Email, string Otp) : IRequest<AuthResponse>;

}
