using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutCatalog.Persistence;

namespace WorkoutCatalog.Features.BrowseWorkouts
{

    public class BrowseWorkoutsQueryHandler : IRequestHandler<BrowseWorkoutsQuery, List<WorkoutResponse>>
    {
        private readonly WorkoutCatalogDbContext _context;

        public BrowseWorkoutsQueryHandler(WorkoutCatalogDbContext context) => _context = context;

        public async Task<List<WorkoutResponse>> Handle(BrowseWorkoutsQuery query, CancellationToken cancellationToken)
        {
            var workouts = _context.Workouts
                .Include(w => w.WorkoutExercises)
                .AsQueryable();

            if (query.Category.HasValue)
                workouts = workouts.Where(w => (int)w.Category == query.Category.Value);

            if (query.Difficulty.HasValue)
                workouts = workouts.Where(w => (int)w.Difficulty == query.Difficulty.Value);

            if (query.Duration.HasValue)
                workouts = workouts.Where(w => w.DurationMinutes <= query.Duration.Value);

            if (!string.IsNullOrEmpty(query.Search))
                workouts = workouts.Where(w => w.Name.Contains(query.Search) || w.Description.Contains(query.Search));

            return await workouts
                .OrderBy(w => w.OrderIndex)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(w => new WorkoutResponse(
                    w.Id, w.Name, w.Description, w.Category.ToString(), w.Difficulty.ToString(),
                    w.DurationMinutes, w.OrderIndex, w.WorkoutExercises.Count))
                .ToListAsync(cancellationToken);
        }
    }
}
