using MediatR;

namespace WorkoutCatalog.Features.GetWorkoutDetail
{

    public record GetWorkoutDetailQuery(Guid Id) : IRequest<WorkoutDetailResponse?>;

    public record WorkoutDetailResponse(
        Guid Id,
        string Name,
        string Description,
        string Category,
        string Difficulty,
        int DurationMinutes,
        int OrderIndex,
        List<ExerciseItem> Exercises
    );

    public record ExerciseItem(
        Guid ExerciseId,
        string Name,
        string TargetMuscles,
        string Equipment,
        int Sets,
        int Reps,
        int? RestSeconds,
        int OrderIndex
    );
}
