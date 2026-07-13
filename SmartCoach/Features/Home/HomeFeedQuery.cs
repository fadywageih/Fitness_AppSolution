using MediatR;

namespace SmartCoach.Features.Home
{

    public record HomeFeedQuery : IRequest<HomeFeedResponse>;

    public record HomeFeedResponse(
        string Greeting,
        string DailyTip,
        object? WorkoutPlan,
        object? MealPlan,
        object? Progress
    );
}
