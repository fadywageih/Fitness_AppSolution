using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutCatalog.Persistence;

namespace WorkoutCatalog.Features.BrowseExercises
{

    public class BrowseExercisesQueryHandler : IRequestHandler<BrowseExercisesQuery, List<ExerciseResponse>>
    {
        private readonly WorkoutCatalogDbContext _context;
        public BrowseExercisesQueryHandler(WorkoutCatalogDbContext context) => _context = context;

        public async Task<List<ExerciseResponse>> Handle(BrowseExercisesQuery query, CancellationToken ct)
            => await _context.Exercises.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize)
                .Select(e => new ExerciseResponse(e.Id, e.Name, e.TargetMuscles, e.Equipment, e.Description)).ToListAsync(ct);
    }
}
