using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Domain.Entities;
using ProfileService.Persistence;
using System.Security.Claims;

namespace ProfileService.Features.UpdateProfile
{

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UpdateProfileResponse?>
    {
        private readonly ProfileDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateProfileCommandHandler(ProfileDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateProfileResponse?> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return null;

            var profile = await _context.Profiles
                .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);

            if (profile is null)
            {
                profile = UserProfile.Create(userId, command.FirstName, command.LastName, command.Email, command.PhoneNumber);
                _context.Profiles.Add(profile);

                var preferences = UserPreference.Create(userId);
                _context.Preferences.Add(preferences);
            }
            else
            {
                profile.Update(command.FirstName, command.LastName, command.Email, command.PhoneNumber);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateProfileResponse(profile.UserId, profile.FirstName, profile.LastName, profile.Email, profile.PhoneNumber);
        }
    }
}
