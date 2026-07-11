using IdentityService.Common.Errors;
using IdentityService.Contracts.Responses;
using IdentityService.Persistence;
using IdentityService.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace IdentityService.Features.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, AuthResponse>
{
    private readonly IdentityDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public ResetPasswordCommandHandler(IdentityDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponse> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var otpCode = await _context.OtpCodes
            .FirstOrDefaultAsync(o =>
                o.ResetToken == command.ResetToken &&
                o.IsUsed &&
                o.ExpiresAt > now,
                cancellationToken);

        if (otpCode is null)
        {
            return AuthResponse.Failure(
                IdentityErrors.ResetTokenInvalid.Message, 400,
                new[] { IdentityErrors.ResetTokenInvalid.Code.ToString() });
        }
        var user = await _context.Users.FindAsync(new object[] { otpCode.UserId }, cancellationToken);

        if (user is null)
        {
            return AuthResponse.Failure(
                IdentityErrors.UserNotFound.Message, 404,
                new[] { IdentityErrors.UserNotFound.Code.ToString() });
        }
        var newPasswordHash = _passwordHasher.Hash(command.NewPassword);
        user.UpdatePassword(newPasswordHash);
        var activeTokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == user.Id && rt.RevokedAt == null)
            .ToListAsync(cancellationToken);


        foreach (var token in activeTokens)
        {
            token.Revoke();
        }
        otpCode.InvalidateResetToken();

        await _context.SaveChangesAsync(cancellationToken);

        return AuthResponse.Success(new { passwordChanged = true }, "Password reset successfully");
    }
}