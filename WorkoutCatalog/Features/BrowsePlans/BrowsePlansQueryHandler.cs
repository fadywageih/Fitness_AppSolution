using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutCatalog.Persistence;

namespace WorkoutCatalog.Features.BrowsePlans
{

    public class BrowsePlansQueryHandler : IRequestHandler<BrowsePlansQuery, List<PlanResponse>>
    {
        private readonly WorkoutCatalogDbContext _context;
        public BrowsePlansQueryHandler(WorkoutCatalogDbContext context) => _context = context;

        public async Task<List<PlanResponse>> Handle(BrowsePlansQuery query, CancellationToken ct)
            => await _context.Plans.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize)
                .Select(p => new PlanResponse(p.Id, p.Name, p.Description, p.Difficulty.ToString(), p.DurationWeeks)).ToListAsync(ct);
    }
}
