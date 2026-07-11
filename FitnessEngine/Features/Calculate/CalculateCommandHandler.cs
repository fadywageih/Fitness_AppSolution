using FitnessEngine.Domain.Entities;
using FitnessEngine.Domain.Enums;
using FitnessEngine.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FitnessEngine.Features.Calculate
{

    public class CalculateCommandHandler : IRequestHandler<CalculateCommand, CalculateResponse?>
    {
        private readonly FitnessDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CalculateCommandHandler(FitnessDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CalculateResponse?> Handle(CalculateCommand command, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId)) return null;

            var stats = await _context.FitnessStats
                .OrderByDescending(s => s.RecordedAt)
                .FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);

            if (stats is null) return null;

            decimal bmr = stats.Gender == "Male"
                ? 10m * stats.Weight + 6.25m * stats.Height - 5 * stats.Age + 5
                : 10m * stats.Weight + 6.25m * stats.Height - 5 * stats.Age - 161;

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

            FitnessStatus status = calorieTarget switch
            {
                <= 1800 => FitnessStatus.Weak,
                <= 2500 => FitnessStatus.Normal,
                _ => FitnessStatus.Hard
            };

            var metrics = await _context.Metrics.FirstOrDefaultAsync(m => m.UserId == userId, cancellationToken);
            if (metrics is null)
            {
                metrics = CalculatedMetrics.Create(userId, bmr, tdee, calorieTarget, status);
                _context.Metrics.Add(metrics);
            }
            else
            {
                metrics.Update(bmr, tdee, calorieTarget, status);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new CalculateResponse(bmr, tdee, calorieTarget, status.ToString());
        }
    }

}
