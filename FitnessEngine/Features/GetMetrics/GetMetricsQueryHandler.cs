using FitnessEngine.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FitnessEngine.Features.GetMetrics
{

    public class GetMetricsQueryHandler : IRequestHandler<GetMetricsQuery, GetMetricsResponse?>
    {
        private readonly FitnessDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetMetricsQueryHandler(FitnessDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetMetricsResponse?> Handle(GetMetricsQuery query, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId)) return null;

            var metrics = await _context.Metrics.FirstOrDefaultAsync(m => m.UserId == userId, cancellationToken);
            if (metrics is null) return null;

            return new GetMetricsResponse(metrics.Bmr, metrics.Tdee, metrics.CalorieTarget, metrics.Status.ToString(), metrics.CalculatedAt);
        }
    }

}
