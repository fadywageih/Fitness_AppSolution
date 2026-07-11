using IdentityService.Common.Errors;
using IdentityService.Contracts.Responses;
using IdentityService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IdentityService.Features.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, AuthResponse>
    {
        private readonly IdentityDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutCommandHandler(IdentityDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthResponse> Handle(LogoutCommand command, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return AuthResponse.Failure(
                    IdentityErrors.TokenInvalid.Message, 401,
                    new[] { IdentityErrors.TokenInvalid.Code.ToString() });
            }

            var activeTokens = await _context.RefreshTokens
                .Where(rt => rt.UserId == userId && rt.RevokedAt == null)
                .ToListAsync(cancellationToken);

            foreach (var token in activeTokens)
            {
                token.Revoke();
            }

            await _context.SaveChangesAsync(cancellationToken);

            return AuthResponse.Success(new { loggedOut = true }, "Logged out successfully");
        }
    }

}
