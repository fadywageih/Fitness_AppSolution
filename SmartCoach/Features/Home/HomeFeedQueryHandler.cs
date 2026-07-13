using MediatR;

namespace SmartCoach.Features.Home
{

    public class HomeFeedQueryHandler : IRequestHandler<HomeFeedQuery, HomeFeedResponse>
    {
        public Task<HomeFeedResponse> Handle(HomeFeedQuery query, CancellationToken ct)
        {
            var hour = DateTime.UtcNow.AddHours(3).Hour;
            var greeting = hour switch
            {
                < 12 => "Good Morning! Ready to crush your workout?",
                < 17 => "Good Afternoon! Time to stay active!",
                _ => "Good Evening! Don't forget to log your progress!"
            };

            var tips = new[]
            {
            "Stay hydrated! Drink at least 8 glasses of water today.",
            "Don't skip your warm-up - it prevents injuries!",
            "Protein intake is key for muscle recovery.",
            "Rest days are just as important as workout days.",
            "Consistency beats intensity every time!"
        };

            return Task.FromResult(new HomeFeedResponse(
                greeting,
                tips[Random.Shared.Next(tips.Length)],
                null, null, null
            ));
        }
    }
}
