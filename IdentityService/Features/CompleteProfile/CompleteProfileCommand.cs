using IdentityService.Contracts.Responses;
using MediatR;

namespace IdentityService.Features.CompleteProfile
{
    public record CompleteProfileCommand : IRequest<AuthResponse>;

}
