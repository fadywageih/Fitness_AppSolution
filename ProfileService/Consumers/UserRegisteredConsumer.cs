using MassTransit;
using ProfileService.Domain.Entities;
using ProfileService.Persistence;
using SharedContracts.Events;

namespace ProfileService.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegisteredEvent>
    {
        private readonly ProfileDbContext _context;

        public UserRegisteredConsumer(ProfileDbContext context) => _context = context;

        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            var msg = context.Message;
            var profile = UserProfile.Create(msg.UserId, msg.FirstName, msg.LastName, msg.Email, msg.PhoneNumber);
            _context.Profiles.Add(profile);

            var preferences = UserPreference.Create(msg.UserId);
            _context.Preferences.Add(preferences);

            await _context.SaveChangesAsync();
        }
    }

}
