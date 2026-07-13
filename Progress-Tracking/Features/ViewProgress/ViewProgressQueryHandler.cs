using MediatR;
using Microsoft.EntityFrameworkCore;
using Progress_Tracking.Persistence;
using System.Security.Claims;

namespace Progress_Tracking.Features.ViewProgress
{
    public class ViewProgressQueryHandler : IRequestHandler<ViewProgressQuery, ViewProgressResponse?>
    {
        private readonly ProgressDbContext _context;
        private readonly IHttpContextAccessor _http;

        public ViewProgressQueryHandler(ProgressDbContext context, IHttpContextAccessor http) { _context = context; _http = http; }

        public async Task<ViewProgressResponse?> Handle(ViewProgressQuery query, CancellationToken ct)
        {
            var userIdClaim = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId)) return null;

            var stats = await _context.UserStatistics.FirstOrDefaultAsync(s => s.UserId == userId, ct);

            var weightQ = _context.WeightHistory.Where(w => w.UserId == userId);
            var workoutQ = _context.WorkoutLogs.Where(w => w.UserId == userId);

            if (query.StartDate.HasValue) { weightQ = weightQ.Where(w => w.Date >= query.StartDate.Value); workoutQ = workoutQ.Where(w => w.CompletedAt >= query.StartDate.Value); }
            if (query.EndDate.HasValue) { weightQ = weightQ.Where(w => w.Date <= query.EndDate.Value); workoutQ = workoutQ.Where(w => w.CompletedAt <= query.EndDate.Value); }

            var weights = await weightQ.OrderBy(w => w.Date).Select(w => new WeightEntry(w.Id, w.Weight, w.Date)).ToListAsync(ct);
            var workouts = await workoutQ.OrderByDescending(w => w.CompletedAt).Select(w => new WorkoutEntry(w.Id, w.WorkoutId, w.CompletedAt, w.DurationMinutes, w.CaloriesBurned, w.Rating)).ToListAsync(ct);

            return new ViewProgressResponse(
                stats?.TotalWorkouts ?? 0, stats?.TotalCaloriesBurned ?? 0,
                stats?.CurrentStreak ?? 0, stats?.LongestStreak ?? 0,
                weights, workouts
            );
        }
    }
}
