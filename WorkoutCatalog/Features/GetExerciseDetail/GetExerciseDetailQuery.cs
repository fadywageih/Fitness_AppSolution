using MediatR;

namespace WorkoutCatalog.Features.GetExerciseDetail
{

    public record GetExerciseDetailQuery(Guid Id) : IRequest<ExerciseDetailResponse?>;

    public record ExerciseDetailResponse(Guid Id, string Name, string TargetMuscles, string Equipment, string Description, string? VideoUrl, string? ImageUrl);
}
