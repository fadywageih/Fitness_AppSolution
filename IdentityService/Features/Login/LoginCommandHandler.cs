using IdentityService.Common.Errors;
using IdentityService.Contracts.Responses;
using IdentityService.Domain.Entities;
using IdentityService.Persistence;
using IdentityService.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Features.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IdentityDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(
        IdentityDbContext context,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == command.Email.ToLowerInvariant(), cancellationToken);

        if (user is null)
        {
            return AuthResponse.Failure(
                IdentityErrors.InvalidCredentials.Message,
                401,
                new[] { IdentityErrors.InvalidCredentials.Code.ToString() });
        }

        if (user.IsLockoutActive())
        {
            return AuthResponse.Failure(
                IdentityErrors.AccountLocked.Message,
                423,
                new[] { IdentityErrors.AccountLocked.Code.ToString() });
        }

        if (!_passwordHasher.Verify(command.Password, user.PasswordHash))
        {
            var failedAttempt = LoginAttempt.CreateFailure(user.Id, "Invalid password");
            _context.LoginAttempts.Add(failedAttempt);
            await _context.SaveChangesAsync(cancellationToken);

            await CheckAndLockAccount(user, cancellationToken);

            return AuthResponse.Failure(
                IdentityErrors.InvalidCredentials.Message,
                401,
                new[] { IdentityErrors.InvalidCredentials.Code.ToString() });
        }

        var successAttempt = LoginAttempt.CreateSuccess(user.Id);
        _context.LoginAttempts.Add(successAttempt);
        await _context.SaveChangesAsync(cancellationToken);

        var token = _jwtService.GenerateToken(user.Id, user.Email, user.IsPremium);

        var refreshTokenValue = Guid.NewGuid().ToString("N");
        var refreshTokenHash = BCrypt.Net.BCrypt.HashPassword(refreshTokenValue);
        var refreshToken = IdentityService.Domain.Entities.RefreshToken.Create(user.Id, refreshTokenHash, 7);
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = new LoginResponse(
            token,
            refreshTokenValue,
            !user.RequiresProfileCompletion,
            user.IsPremium);

        return AuthResponse.Success(response, "Login successful");
    }

    private async Task CheckAndLockAccount(User user, CancellationToken cancellationToken)
    {
        var fifteenMinutesAgo = DateTime.UtcNow.AddMinutes(-15);

        var failedAttempts = await _context.LoginAttempts
            .CountAsync(la =>
                la.UserId == user.Id &&
                !la.IsSuccess &&
                la.AttemptedAt >= fifteenMinutesAgo,
                cancellationToken);

        if (failedAttempts >= 5)
        {
            user.LockAccount(15);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}