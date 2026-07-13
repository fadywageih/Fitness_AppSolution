using MediatR;

namespace Progress_Tracking.Features.ViewProgress
{

    public record ViewProgressQuery(string? Period, DateTime? StartDate, DateTime? EndDate) : IRequest<ViewProgressResponse?>;

    public record ViewProgressResponse(
        int TotalWorkouts,
        int TotalCaloriesBurned,
        int CurrentStreak,
        int LongestStreak,
        List<WeightEntry> WeightHistory,
        List<WorkoutEntry> WorkoutHistory
    );

    public record WeightEntry(Guid Id, decimal Weight, DateTime Date);
    public record WorkoutEntry(Guid Id, Guid WorkoutId, DateTime CompletedAt, int DurationMinutes, int CaloriesBurned, int Rating);
}
