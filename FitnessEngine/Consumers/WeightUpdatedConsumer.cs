using FitnessEngine.Domain.Entities;
using FitnessEngine.Domain.Enums;
using FitnessEngine.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedContracts.Events;

namespace FitnessEngine.Consumers
{

    public class WeightUpdatedConsumer : IConsumer<WeightUpdatedEvent>
    {
        private readonly FitnessDbContext _context;

        public WeightUpdatedConsumer(FitnessDbContext context) => _context = context;

        public async Task Consume(ConsumeContext<WeightUpdatedEvent> context)
        {
            var msg = context.Message;
            var stats = await _context.FitnessStats.OrderByDescending(s => s.RecordedAt).FirstOrDefaultAsync(s => s.UserId == msg.UserId);
            if (stats is null) return;

            var metrics = await _context.Metrics.FirstOrDefaultAsync(m => m.UserId == msg.UserId);
            if (metrics is null) return;

            var oldStatus = metrics.Status;

            decimal bmr = stats.Gender == "Male"
                ? 10m * msg.NewWeight + 6.25m * stats.Height - 5 * stats.Age + 5
                : 10m * msg.NewWeight + 6.25m * stats.Height - 5 * stats.Age - 161;

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

            if (oldStatus != newStatus)
            {
                var oldPlan = await _context.AssignedPlans.FirstOrDefaultAsync(p => p.UserId == msg.UserId && p.IsActive);
                if (oldPlan is not null) oldPlan.Deactivate();

                var newPlan = await _context.PlanConfigs.FirstOrDefaultAsync(p => p.Goal == stats.Goal && p.Status == newStatus);
                if (newPlan is null)
                {
                    newPlan = new FitnessPlanConfig(stats.Goal, newStatus, "Auto Plan", 8, 4, "Auto plan");
                    _context.PlanConfigs.Add(newPlan);
                    await _context.SaveChangesAsync();
                }

                _context.AssignedPlans.Add(UserAssignedPlan.Create(msg.UserId, newPlan.Id));
            }

            await _context.SaveChangesAsync();
        }
    }
}
