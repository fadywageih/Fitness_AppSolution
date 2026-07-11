using MediatR;

namespace ProfileService.Features.UploadPicture
{
    public record UploadPictureCommand(IFormFile File) : IRequest<UploadPictureResponse?>;

    public record UploadPictureResponse(string ProfilePictureUrl);
}
