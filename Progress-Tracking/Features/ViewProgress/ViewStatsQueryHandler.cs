using MediatR;
using Microsoft.EntityFrameworkCore;
using Progress_Tracking.Persistence;

namespace Progress_Tracking.Features.ViewProgress
{

    public class ViewStatsQueryHandler : IRequestHandler<ViewStatsQuery, StatsResponse?>
    {
        private readonly ProgressDbContext _context;
        public ViewStatsQueryHandler(ProgressDbContext context) => _context = context;

        public async Task<StatsResponse?> Handle(ViewStatsQuery query, CancellationToken ct)
        {
            var stats = await _context.UserStatistics.FirstOrDefaultAsync(s => s.UserId == query.UserId, ct);
            return stats is null ? null : new StatsResponse(stats.TotalWorkouts, stats.TotalCaloriesBurned, stats.CurrentStreak, stats.LongestStreak);
        }
    }
}
