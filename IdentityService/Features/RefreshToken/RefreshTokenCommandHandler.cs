using IdentityService.Common.Errors;
using IdentityService.Contracts.Responses;
using IdentityService.Features.Login;
using IdentityService.Persistence;
using IdentityService.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Features.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly IdentityDbContext _context;
    private readonly IJwtService _jwtService;

    public RefreshTokenCommandHandler(IdentityDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var allActiveTokens = await _context.RefreshTokens
            .Include(rt => rt.User)
            .Where(rt => rt.RevokedAt == null && rt.ExpiresAt > DateTime.UtcNow)
            .ToListAsync(cancellationToken);
        var existingToken = allActiveTokens
            .FirstOrDefault(rt => BCrypt.Net.BCrypt.Verify(command.RefreshToken, rt.TokenHash));

        if (existingToken is null)
        {
            return AuthResponse.Failure(
                IdentityErrors.TokenInvalid.Message, 401,
                new[] { IdentityErrors.TokenInvalid.Code.ToString() });
        }
        if (existingToken.IsExpired)
        {
            return AuthResponse.Failure(
                IdentityErrors.TokenExpired.Message, 401,
                new[] { IdentityErrors.TokenExpired.Code.ToString() });
        }
        var user = existingToken.User;
        var newToken = _jwtService.GenerateToken(user.Id, user.Email, user.IsPremium);
        var newRefreshTokenValue = Guid.NewGuid().ToString("N");
        var newRefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(newRefreshTokenValue);
        var newRefreshToken = IdentityService.Domain.Entities.RefreshToken.Create(user.Id, newRefreshTokenHash, 7);
        existingToken.Revoke(newRefreshTokenValue);
        _context.RefreshTokens.Add(newRefreshToken);
        await _context.SaveChangesAsync(cancellationToken);
        var response = new LoginResponse(
            newToken,
            newRefreshTokenValue,
            !user.RequiresProfileCompletion,
            user.IsPremium);

        return AuthResponse.Success(response, "Token refreshed successfully");
    }
}