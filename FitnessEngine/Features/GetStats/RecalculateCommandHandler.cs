using FitnessEngine.Domain.Entities;
using FitnessEngine.Domain.Enums;
using FitnessEngine.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FitnessEngine.Features.GetStats
{

    public class RecalculateCommandHandler : IRequestHandler<RecalculateCommand, RecalculateResponse?>
    {
        private readonly FitnessDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecalculateCommandHandler(FitnessDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RecalculateResponse?> Handle(RecalculateCommand command, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId)) return null;

            var stats = await _context.FitnessStats.OrderByDescending(s => s.RecordedAt).FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);
            if (stats is null) return null;

            var metrics = await _context.Metrics.FirstOrDefaultAsync(m => m.UserId == userId, cancellationToken);
            if (metrics is null) return null;

            var oldStatus = metrics.Status;

            decimal weight = command.NewWeight ?? stats.Weight;

            decimal bmr = stats.Gender == "Male"
                ? 10m * weight + 6.25m * stats.Height - 5 * stats.Age + 5
                : 10m * weight + 6.25m * stats.Height - 5 * stats.Age - 161;

            decimal activityFactor = stats.ActivityLevel switch
            {
                ActivityLevel.Rookie => 1.2m,
                ActivityLevel.Beginner => 1.375m,
                ActivityLevel.Intermediate => 1.55m,
                ActivityLevel.Advance => 1.725m,
                ActivityLevel.TrueBeast => 1.9m,
                _ => 1.2m
            };

            decimal tdee = bmr * activityFactor;
            decimal calorieTarget = stats.Goal switch
            {
                FitnessGoal.LoseWeight => tdee - 500,
                FitnessGoal.GainWeight => tdee + 300,
                FitnessGoal.GainMoreFlexible => tdee + 150,
                _ => tdee
            };

            FitnessStatus newStatus = calorieTarget switch
            {
                <= 1800 => FitnessStatus.Weak,
                <= 2500 => FitnessStatus.Normal,
                _ => FitnessStatus.Hard
            };

            metrics.Update(bmr, tdee, calorieTarget, newStatus);
            bool reassigned = false;

            if (oldStatus != newStatus)
            {
                var oldPlan = await _context.AssignedPlans.FirstOrDefaultAsync(p => p.UserId == userId && p.IsActive, cancellationToken);
                if (oldPlan is not null) oldPlan.Deactivate();

                var newPlan = await _context.PlanConfigs.FirstOrDefaultAsync(p => p.Goal == stats.Goal && p.Status == newStatus, cancellationToken);
                if (newPlan is null)
                {
                    newPlan = new FitnessPlanConfig(stats.Goal, newStatus, "Adjusted Plan", 8, 4, "Auto-adjusted plan");
                    _context.PlanConfigs.Add(newPlan);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                _context.AssignedPlans.Add(UserAssignedPlan.Create(userId, newPlan.Id));
                reassigned = true;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return new RecalculateResponse(bmr, tdee, calorieTarget, newStatus.ToString(), reassigned);
        }
    }
}
