using MediatR;

namespace Progress_Tracking.Features.Achievements
{

    public record AchievementsQuery : IRequest<List<AchievementResponse>>;

    public record AchievementResponse(string Name, string Description, string IconUrl, DateTime EarnedAt);
}
