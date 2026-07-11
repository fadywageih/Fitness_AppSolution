using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Persistence;
using System.Security.Claims;

namespace ProfileService.Features.ViewProfile
{
    public class ViewProfileQueryHandler : IRequestHandler<ViewProfileQuery, ViewProfileResponse?>
    {
        private readonly ProfileDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ViewProfileQueryHandler(ProfileDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ViewProfileResponse?> Handle(ViewProfileQuery query, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return null;

            var profile = await _context.Profiles
                .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);

            if (profile is null)
                return null;

            var preferences = await _context.Preferences
                .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);

            return new ViewProfileResponse(
                profile.UserId,
                profile.FirstName,
                profile.LastName,
                profile.Email,
                profile.PhoneNumber,
                profile.ProfilePictureUrl,
                preferences?.Theme ?? "light",
                preferences?.Language ?? "en",
                preferences?.WorkoutReminders ?? true
            );
        }
    }
}
