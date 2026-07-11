using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Persistence;
using System.Security.Claims;

namespace ProfileService.Features.ViewSettings
{

    public class ViewSettingsQueryHandler : IRequestHandler<ViewSettingsQuery, ViewSettingsResponse?>
    {
        private readonly ProfileDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ViewSettingsQueryHandler(ProfileDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ViewSettingsResponse?> Handle(ViewSettingsQuery query, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return null;

            var preferences = await _context.Preferences
                .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);

            if (preferences is null)
                return null;

            return new ViewSettingsResponse(preferences.Theme, preferences.Language, preferences.WorkoutReminders);
        }
    }
}
