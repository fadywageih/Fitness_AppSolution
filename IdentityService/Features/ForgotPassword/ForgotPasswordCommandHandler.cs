using IdentityService.Common.Errors;
using IdentityService.Contracts.Responses;
using IdentityService.Domain.Entities;
using IdentityService.Persistence;
using IdentityService.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Features.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, AuthResponse>
{
    private readonly IdentityDbContext _context;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(IdentityDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<AuthResponse> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == command.Email.ToLowerInvariant(), cancellationToken);

        if (user is null)
        {
            return AuthResponse.Failure(
                IdentityErrors.UserNotFound.Message,
                404,
                new[] { IdentityErrors.UserNotFound.Code.ToString() });
        }

        var thirtySecondsAgo = DateTime.UtcNow.AddSeconds(-30);
        var recentOtp = await _context.OtpCodes
            .AnyAsync(o =>
                o.UserId == user.Id &&
                o.Purpose == "PasswordReset" &&
                o.CreatedAt >= thirtySecondsAgo,
                cancellationToken);

        if (recentOtp)
        {
            return AuthResponse.Failure(
                IdentityErrors.OtpResendTooSoon.Message,
                429,
                new[] { IdentityErrors.OtpResendTooSoon.Code.ToString() });
        }

        var otp = new Random().Next(100000, 999999).ToString();
        var otpHash = BCrypt.Net.BCrypt.HashPassword(otp);
        var otpCode = OtpCode.Create(user.Id, otpHash, "PasswordReset");
        _context.OtpCodes.Add(otpCode);
        await _context.SaveChangesAsync(cancellationToken);

        // Send email with OTP
        await _emailService.SendEmailAsync(
            user.Email,
            "Password Reset OTP - Elevate Fitness",
            $"<h1>Your OTP Code</h1><p>Your verification code is: <strong>{otp}</strong></p><p>This code expires in 10 minutes.</p>"
        );

        Console.WriteLine($"OTP for {user.Email}: {otp}");

        return AuthResponse.Success(new
        {
            otpExpiresIn = 600,
            canResendIn = 30
        }, "OTP sent successfully");
    }
}