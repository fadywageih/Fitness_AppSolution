using MediatR;
using Microsoft.EntityFrameworkCore;
using Progress_Tracking.Domain.Entities;
using Progress_Tracking.Persistence;
using System.Security.Claims;

namespace Progress_Tracking.Features.LogWorkout
{

    public class LogWorkoutCommandHandler : IRequestHandler<LogWorkoutCommand, LogWorkoutResponse?>
    {
        private readonly ProgressDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogWorkoutCommandHandler(ProgressDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LogWorkoutResponse?> Handle(LogWorkoutCommand command, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId)) return null;

            if (command.Rating < 1 || command.Rating > 5) return null;

            var log = WorkoutLog.Create(userId, command.WorkoutId, command.SessionId, command.CompletedAt,
                command.DurationMinutes, command.CaloriesBurned, command.Difficulty, command.Notes, command.Rating);
            _context.WorkoutLogs.Add(log);

            var stats = await _context.UserStatistics.FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);
            if (stats is null)
            {
                stats = UserStatistics.Create(userId);
                _context.UserStatistics.Add(stats);
            }

            stats.AddWorkout(command.CaloriesBurned, command.CompletedAt);
            await _context.SaveChangesAsync(cancellationToken);

            return new LogWorkoutResponse(log.Id, stats.CurrentStreak, stats.TotalWorkouts);
        }
    }
}
