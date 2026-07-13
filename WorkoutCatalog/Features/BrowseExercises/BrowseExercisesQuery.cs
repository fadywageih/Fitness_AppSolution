using MediatR;

namespace WorkoutCatalog.Features.BrowseExercises
{

    public record BrowseExercisesQuery(int Page = 1, int PageSize = 50) : IRequest<List<ExerciseResponse>>;

    public record ExerciseResponse(Guid Id, string Name, string TargetMuscles, string Equipment, string Description);
}
