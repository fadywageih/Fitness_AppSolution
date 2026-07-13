using MediatR;
using Microsoft.EntityFrameworkCore;
using Progress_Tracking.Persistence;

namespace Progress_Tracking.Features.ViewProgress
{

    public class ViewUserProgressQueryHandler : IRequestHandler<ViewUserProgressQuery, ViewProgressResponse?>
    {
        private readonly ProgressDbContext _context;

        public ViewUserProgressQueryHandler(ProgressDbContext context) => _context = context;

        public async Task<ViewProgressResponse?> Handle(ViewUserProgressQuery query, CancellationToken ct)
        {
            var stats = await _context.UserStatistics.FirstOrDefaultAsync(s => s.UserId == query.UserId, ct);
            var weights = await _context.WeightHistory.Where(w => w.UserId == query.UserId).OrderBy(w => w.Date)
                .Select(w => new WeightEntry(w.Id, w.Weight, w.Date)).ToListAsync(ct);
            var workouts = await _context.WorkoutLogs.Where(w => w.UserId == query.UserId).OrderByDescending(w => w.CompletedAt)
                .Select(w => new WorkoutEntry(w.Id, w.WorkoutId, w.CompletedAt, w.DurationMinutes, w.CaloriesBurned, w.Rating)).ToListAsync(ct);

            return new ViewProgressResponse(stats?.TotalWorkouts ?? 0, stats?.TotalCaloriesBurned ?? 0, stats?.CurrentStreak ?? 0, stats?.LongestStreak ?? 0, weights, workouts);
        }
    }
}
