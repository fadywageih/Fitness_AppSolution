using MediatR;

namespace Progress_Tracking.Features.ViewProgress
{

    public record ViewStatsQuery(Guid UserId) : IRequest<StatsResponse?>;

    public record StatsResponse(int TotalWorkouts, int TotalCaloriesBurned, int CurrentStreak, int LongestStreak);
}
