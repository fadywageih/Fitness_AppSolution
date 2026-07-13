using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutCatalog.Persistence;

namespace WorkoutCatalog.Features.GetWorkoutDetail
{

    public class GetWorkoutDetailQueryHandler : IRequestHandler<GetWorkoutDetailQuery, WorkoutDetailResponse?>
    {
        private readonly WorkoutCatalogDbContext _context;

        public GetWorkoutDetailQueryHandler(WorkoutCatalogDbContext context) => _context = context;

        public async Task<WorkoutDetailResponse?> Handle(GetWorkoutDetailQuery query, CancellationToken cancellationToken)
        {
            var workout = await _context.Workouts
                .Include(w => w.WorkoutExercises)
                .ThenInclude(we => we.Exercise)
                .FirstOrDefaultAsync(w => w.Id == query.Id, cancellationToken);

            if (workout is null) return null;

            return new WorkoutDetailResponse(
                workout.Id, workout.Name, workout.Description,
                workout.Category.ToString(), workout.Difficulty.ToString(),
                workout.DurationMinutes, workout.OrderIndex,
                workout.WorkoutExercises.OrderBy(e => e.OrderIndex).Select(e => new ExerciseItem(
                    e.ExerciseId, e.Exercise.Name, e.Exercise.TargetMuscles,
                    e.Exercise.Equipment, e.Sets, e.Reps, e.RestSeconds, e.OrderIndex
                )).ToList()
            );
        }
    }
}
