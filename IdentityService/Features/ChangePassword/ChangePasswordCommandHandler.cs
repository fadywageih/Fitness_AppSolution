using IdentityService.Contracts.Responses;
using IdentityService.Persistence;
using IdentityService.Services;
using MediatR;
using System.Security.Claims;

namespace IdentityService.Features.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, AuthResponse>
    {
        private readonly IdentityDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChangePasswordCommandHandler(
            IdentityDbContext context,
            IPasswordHasher passwordHasher,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthResponse> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return AuthResponse.Failure("Unauthorized", 401);

            var user = await _context.Users.FindAsync(new object[] { userId }, cancellationToken);

            if (user is null)
                return AuthResponse.Failure("User not found", 404);

            if (!_passwordHasher.Verify(command.CurrentPassword, user.PasswordHash))
                return AuthResponse.Failure("Current password is incorrect", 401);

            var newHash = _passwordHasher.Hash(command.NewPassword);
            user.UpdatePassword(newHash);
            await _context.SaveChangesAsync(cancellationToken);

            return AuthResponse.Success(new { passwordChanged = true }, "Password changed successfully");
        }
    }
}
