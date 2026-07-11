using IdentityService.Common.Errors;
using IdentityService.Contracts.Responses;
using IdentityService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Features.VerifyOtp;

public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, AuthResponse>
{
    private readonly IdentityDbContext _context;

    public VerifyOtpCommandHandler(IdentityDbContext context)
    {
        _context = context;
    }
    public async Task<AuthResponse> Handle(VerifyOtpCommand command, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == command.Email.ToLowerInvariant(), cancellationToken);

        if (user is null)
        {
            return AuthResponse.Failure(
                IdentityErrors.UserNotFound.Message, 404,
                new[] { IdentityErrors.UserNotFound.Code.ToString() });
        }
        var otpCode = await _context.OtpCodes
            .Where(o => o.UserId == user.Id && o.Purpose == "PasswordReset" && !o.IsUsed)
            .OrderByDescending(o => o.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (otpCode is null || otpCode.IsExpired())
        {
            return AuthResponse.Failure(
                IdentityErrors.OtpExpired.Message, 400,
                new[] { IdentityErrors.OtpExpired.Code.ToString() });
        }
        if (!BCrypt.Net.BCrypt.Verify(command.Otp, otpCode.CodeHash))
        {
            return AuthResponse.Failure(
                IdentityErrors.InvalidOtp.Message, 400,
                new[] { IdentityErrors.InvalidOtp.Code.ToString() });
        }
        var resetToken = Guid.NewGuid().ToString("N");
        otpCode.SetResetToken(resetToken);
        otpCode.MarkAsUsed();
        await _context.SaveChangesAsync(cancellationToken);

        return AuthResponse.Success(new { resetToken }, "OTP verified successfully");
    }
}