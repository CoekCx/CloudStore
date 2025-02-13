using Common.Abstractions.Contracts;
using MediatR;
using UserAdministration.Business.Abstractions;
using UserAdministration.Domain.Entities;

namespace UserAdministration.Business.Users.Create;

public sealed class CreateUserCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher)
    : IRequestHandler<CreateUserCommand, EntityCreatedResponse>
{
    public async Task<EntityCreatedResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.Email, request.FirstName, request.LastName);

        var passwordHash = passwordHasher.HashPassword(user.Id, request.Password);

        user.UpdatePasswordHash(passwordHash);

        context.Users.Add(user);

        await context.SaveChangesAsync(cancellationToken);

        return new EntityCreatedResponse(user.Id);
    }
}