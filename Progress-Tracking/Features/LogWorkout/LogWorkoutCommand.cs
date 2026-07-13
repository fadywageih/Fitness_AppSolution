using MediatR;

namespace Progress_Tracking.Features.LogWorkout
{

    public record LogWorkoutCommand(
        Guid WorkoutId,
        Guid SessionId,
        DateTime CompletedAt,
        int DurationMinutes,
        int CaloriesBurned,
        string? Difficulty,
        string? Notes,
        int Rating
    ) : IRequest<LogWorkoutResponse?>;

    public record LogWorkoutResponse(Guid LogId, int CurrentStreak, int TotalWorkouts);

}
