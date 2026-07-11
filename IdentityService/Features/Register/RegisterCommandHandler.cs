using IdentityService.Common.Errors;
using IdentityService.Contracts.Responses;
using IdentityService.Domain.Entities;
using IdentityService.Persistence;
using IdentityService.Services;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedContracts.Events;

namespace IdentityService.Features.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IdentityDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPublishEndpoint _publishEndpoint;

    public RegisterCommandHandler(IdentityDbContext context, IPasswordHasher passwordHasher, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<AuthResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var emailExists = await _context.Users.AnyAsync(u => u.Email == command.Email.ToLowerInvariant(), cancellationToken);
        if (emailExists)
            return AuthResponse.Failure(IdentityErrors.EmailAlreadyExists.Message, 409, new[] { IdentityErrors.EmailAlreadyExists.Code.ToString() });

        var passwordHash = _passwordHasher.Hash(command.Password);
        var user = User.Create(command.FirstName, command.LastName, command.Email, passwordHash, command.PhoneNumber);
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        await _publishEndpoint.Publish(new UserRegisteredEvent(user.Id, user.FirstName, user.LastName, user.Email, user.PhoneNumber), cancellationToken);

        return AuthResponse.Created(new RegisterResponse(user.Id, user.RequiresProfileCompletion), "Registration successful");
    }
}