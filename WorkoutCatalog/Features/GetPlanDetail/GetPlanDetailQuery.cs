using MediatR;

namespace WorkoutCatalog.Features.GetPlanDetail
{

    public record GetWorkoutPlanDetailQuery(Guid Id) : IRequest<PlanDetailResponse?>;

    public record PlanDetailResponse(
        Guid Id,
        string Name,
        string Description,
        string Difficulty,
        int DurationWeeks,
        List<PlanWorkoutItem> Workouts
    );

    public record PlanWorkoutItem(Guid WorkoutId, string Name, string Category, int DurationMinutes);
}
