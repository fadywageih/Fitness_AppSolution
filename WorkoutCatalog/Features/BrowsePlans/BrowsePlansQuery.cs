using MediatR;

namespace WorkoutCatalog.Features.BrowsePlans
{

    public record BrowsePlansQuery(int Page = 1, int PageSize = 20) : IRequest<List<PlanResponse>>;

    public record PlanResponse(Guid Id, string Name, string Description, string Difficulty, int DurationWeeks);
}
