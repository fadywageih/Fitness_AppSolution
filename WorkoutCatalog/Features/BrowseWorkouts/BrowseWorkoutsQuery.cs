using MediatR;

namespace WorkoutCatalog.Features.BrowseWorkouts
{

    public record BrowseWorkoutsQuery(
        int? Category,
        int? Difficulty,
        int? Duration,
        string? Search,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<List<WorkoutResponse>>;

    public record WorkoutResponse(
        Guid Id,
        string Name,
        string Description,
        string Category,
        string Difficulty,
        int DurationMinutes,
        int OrderIndex,
        int ExerciseCount
    );
}
