using FitnessEngine.Domain.Entities;
using FitnessEngine.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FitnessEngine.Features.AssignPlan
{

    public class AssignPlanCommandHandler : IRequestHandler<AssignPlanCommand, AssignPlanResponse?>
    {
        private readonly FitnessDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AssignPlanCommandHandler(FitnessDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AssignPlanResponse?> Handle(AssignPlanCommand command, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId)) return null;

            var metrics = await _context.Metrics.FirstOrDefaultAsync(m => m.UserId == userId, cancellationToken);
            if (metrics is null) return null;

            var stats = await _context.FitnessStats.OrderByDescending(s => s.RecordedAt).FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);
            if (stats is null) return null;

            var plan = await _context.PlanConfigs.FirstOrDefaultAsync(p => p.Goal == stats.Goal && p.Status == metrics.Status, cancellationToken);

            if (plan is null)
            {
                plan = new FitnessPlanConfig(stats.Goal, metrics.Status, "Default Plan", 8, 4, "Customized fitness plan");
                _context.PlanConfigs.Add(plan);
                await _context.SaveChangesAsync(cancellationToken);
            }

            var existingPlan = await _context.AssignedPlans.FirstOrDefaultAsync(p => p.UserId == userId && p.IsActive, cancellationToken);
            if (existingPlan is not null) existingPlan.Deactivate();

            var assignedPlan = UserAssignedPlan.Create(userId, plan.Id);
            _context.AssignedPlans.Add(assignedPlan);
            await _context.SaveChangesAsync(cancellationToken);

            return new AssignPlanResponse(plan.Id, plan.PlanName, plan.DurationWeeks, plan.WorkoutsPerWeek);
        }
    }
}
