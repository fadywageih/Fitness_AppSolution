using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Domain.Entities;
using ProfileService.Persistence;
using System.Security.Claims;

namespace ProfileService.Features.UpdateSettings
{
    public class UpdateSettingsCommandHandler : IRequestHandler<UpdateSettingsCommand, UpdateSettingsResponse?>
    {
        private readonly ProfileDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateSettingsCommandHandler(ProfileDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateSettingsResponse?> Handle(UpdateSettingsCommand command, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return null;

            var preferences = await _context.Preferences
                .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);

            if (preferences is null)
            {
                preferences = UserPreference.Create(userId);
                _context.Preferences.Add(preferences);
            }

            if (command.Theme is not null)
                preferences.UpdateTheme(command.Theme);

            if (command.Language is not null)
                preferences.UpdateLanguage(command.Language);

            if (command.WorkoutReminders.HasValue)
                preferences.UpdateWorkoutReminders(command.WorkoutReminders.Value);

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateSettingsResponse(preferences.Theme, preferences.Language, preferences.WorkoutReminders);
        }
    }
}
