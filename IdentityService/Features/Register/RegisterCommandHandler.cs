using IdentityService.Common.Errors;
using IdentityService.Contracts.Responses;
using IdentityService.Domain.Entities;
using IdentityService.Persistence;
using IdentityService.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Features.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IdentityDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterCommandHandler(IdentityDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var emailExists = await _context.Users
                .AnyAsync(u => u.Email == command.Email.ToLowerInvariant(), cancellationToken);

            if (emailExists)
            {
                var error = IdentityErrors.EmailAlreadyExists;
                return AuthResponse.Failure(error.Message, 409, new[] { error.Code.ToString() });
            }

            var passwordHash = _passwordHasher.Hash(command.Password);
            var user = User.Create(
                command.FirstName,
                command.LastName,
                command.Email,
                passwordHash,
                command.PhoneNumber
            );
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            var response = new RegisterResponse(user.Id, user.RequiresProfileCompletion);
            return AuthResponse.Created(response, "Registration successful");
        }
    }
}
