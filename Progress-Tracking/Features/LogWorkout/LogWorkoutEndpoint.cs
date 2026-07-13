using MediatR;

namespace Progress_Tracking.Features.LogWorkout
{

    public static class LogWorkoutEndpoint
    {
        public static void MapLogWorkoutEndpoint(this WebApplication app)
        {
            app.MapPost("/api/v1/progress/workouts", async (
                Guid workoutId, Guid sessionId, DateTime completedAt, int durationMinutes,
                int caloriesBurned, string? difficulty, string? notes, int rating, ISender sender) =>
            {
                var command = new LogWorkoutCommand(workoutId, sessionId, completedAt, durationMinutes, caloriesBurned, difficulty, notes, rating);
                var result = await sender.Send(command);
                return result is null ? Results.BadRequest(new { message = "Invalid data" })
                    : Results.Ok(new { isSuccess = true, data = result, statusCode = 201 });
            }).WithName("LogWorkout").WithTags("Progress").RequireAuthorization();
        }
    }
}
