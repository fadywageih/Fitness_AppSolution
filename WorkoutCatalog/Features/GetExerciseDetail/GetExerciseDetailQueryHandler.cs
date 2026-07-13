using MediatR;
using WorkoutCatalog.Persistence;

namespace WorkoutCatalog.Features.GetExerciseDetail
{

    public class GetExerciseDetailQueryHandler : IRequestHandler<GetExerciseDetailQuery, ExerciseDetailResponse?>
    {
        private readonly WorkoutCatalogDbContext _context;
        public GetExerciseDetailQueryHandler(WorkoutCatalogDbContext context) => _context = context;

        public async Task<ExerciseDetailResponse?> Handle(GetExerciseDetailQuery query, CancellationToken ct)
        {
            var ex = await _context.Exercises.FindAsync(new object[] { query.Id }, ct);
            return ex is null ? null : new ExerciseDetailResponse(ex.Id, ex.Name, ex.TargetMuscles, ex.Equipment, ex.Description, ex.VideoUrl, ex.ImageUrl);
        }
    }
}
