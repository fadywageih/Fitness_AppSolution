using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Persistence;
using System.Security.Claims;

namespace ProfileService.Features.UploadPicture
{

    public class UploadPictureCommandHandler : IRequestHandler<UploadPictureCommand, UploadPictureResponse?>
    {
        private readonly ProfileDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _environment;

        public UploadPictureCommandHandler(
            ProfileDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment environment)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
        }

        public async Task<UploadPictureResponse?> Handle(UploadPictureCommand command, CancellationToken cancellationToken)
        {
            var file = command.File;

            if (file is null || file.Length == 0)
                return null;

            if (file.Length > 5 * 1024 * 1024)
                return null;

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                return null;

            var userIdClaim = _httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return null;

            var profile = await _context.Profiles
                .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);

            if (profile is null)
                return null;

            var uploadsFolder = Path.Combine(_environment.WebRootPath ?? "wwwroot", "uploads", "profiles");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{userId}_{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            var pictureUrl = $"/uploads/profiles/{fileName}";
            profile.UpdatePicture(pictureUrl);
            await _context.SaveChangesAsync(cancellationToken);

            return new UploadPictureResponse(pictureUrl);
        }
    }

}
