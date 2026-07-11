using FitnessEngine.Domain.Entities;
using FitnessEngine.Domain.Enums;
using FitnessEngine.Persistence;
using MediatR;
using System.Security.Claims;

namespace FitnessEngine.Features.SubmitStats
{

    public class SubmitStatsCommandHandler : IRequestHandler<SubmitStatsCommand, SubmitStatsResponse?>
    {
        private readonly FitnessDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubmitStatsCommandHandler(FitnessDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SubmitStatsResponse?> Handle(SubmitStatsCommand command, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return null;

            var stats = UserFitnessStats.Create(
                userId, command.Weight, command.Height, command.Age,
                command.Gender, (FitnessGoal)command.Goal, (ActivityLevel)command.ActivityLevel);

            _context.FitnessStats.Add(stats);
            await _context.SaveChangesAsync(cancellationToken);

            return new SubmitStatsResponse(userId, "Stats submitted successfully");
        }
    }

}
