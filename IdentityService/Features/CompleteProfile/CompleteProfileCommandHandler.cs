using IdentityService.Common.Errors;
using IdentityService.Contracts.Responses;
using IdentityService.Persistence;
using MediatR;
using System.Security.Claims;

namespace IdentityService.Features.CompleteProfile
{
    public class CompleteProfileCommandHandler : IRequestHandler<CompleteProfileCommand, AuthResponse>
    {
        private readonly IdentityDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompleteProfileCommandHandler(
            IdentityDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthResponse> Handle(CompleteProfileCommand command, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return AuthResponse.Failure(
                    IdentityErrors.TokenInvalid.Message,
                    401,
                    new[] { IdentityErrors.TokenInvalid.Code.ToString() });
            }
            var user = await _context.Users.FindAsync(new object[] { userId }, cancellationToken);

            if (user is null)
            {
                return AuthResponse.Failure(
                    IdentityErrors.UserNotFound.Message,
                    404,
                    new[] { IdentityErrors.UserNotFound.Code.ToString() });
            }
            user.CompleteProfile();
            await _context.SaveChangesAsync(cancellationToken);

            return AuthResponse.Success(null, "Profile completed successfully");
        }
    }

}
