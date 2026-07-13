using MediatR;
using Microsoft.EntityFrameworkCore;
using Progress_Tracking.Persistence;
using System.Security.Claims;

namespace Progress_Tracking.Features.Achievements
{

    public class AchievementsQueryHandler : IRequestHandler<AchievementsQuery, List<AchievementResponse>>
    {
        private readonly ProgressDbContext _context;
        private readonly IHttpContextAccessor _http;

        public AchievementsQueryHandler(ProgressDbContext context, IHttpContextAccessor http) { _context = context; _http = http; }

        public async Task<List<AchievementResponse>> Handle(AchievementsQuery query, CancellationToken ct)
        {
            var userIdClaim = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId)) return new();

            var stats = await _context.UserStatistics.FirstOrDefaultAsync(s => s.UserId == userId, ct);
            if (stats is null) return new();

            var achievements = new List<AchievementResponse>();

            if (stats.TotalWorkouts >= 1)
                achievements.Add(new AchievementResponse("First Workout", "Completed your first workout!", "🏆", DateTime.UtcNow));
            if (stats.TotalWorkouts >= 10)
                achievements.Add(new AchievementResponse("10 Workouts", "Completed 10 workouts!", "🔥", DateTime.UtcNow));
            if (stats.CurrentStreak >= 7)
                achievements.Add(new AchievementResponse("7-Day Streak", "7 days in a row!", "⚡", DateTime.UtcNow));

            return achievements;
        }
    }
}
