using FitnessEngine.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FitnessEngine.Features.GetStats
{

    public class GetStatsQueryHandler : IRequestHandler<GetStatsQuery, GetStatsResponse?>
    {
        private readonly FitnessDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetStatsQueryHandler(FitnessDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetStatsResponse?> Handle(GetStatsQuery query, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId)) return null;

            var stats = await _context.FitnessStats.OrderByDescending(s => s.RecordedAt).FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);
            if (stats is null) return null;

            return new GetStatsResponse(stats.Weight, stats.Height, stats.Age, stats.Gender, stats.Goal.ToString(), stats.ActivityLevel.ToString());
        }
    }
}
