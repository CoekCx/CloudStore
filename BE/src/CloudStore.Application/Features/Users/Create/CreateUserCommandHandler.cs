using CloudStore.Application.Abstractions;
using CloudStore.Application.DTOs.Responses;
using CloudStore.Domain.Abstractions.Repositories.Users;
using CloudStore.Domain.Entities;
using CloudStore.Domain.Exceptions.Users;
using MediatR;

namespace CloudStore.Application.Features.Users.Create;

public sealed class CreateUserCommandHandler(
    IUserWriteRepository writeRepository,
    IUserReadRepository readRepository,
    IPasswordHasher passwordHasher)
    : IRequestHandler<CreateUserCommand, UserCreatedResponse>
{
    public async Task<UserCreatedResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await readRepository.EmailExistsAsync(request.Email);

        if (result) throw new UserEmailAlreadyExists(request.Email);

        var user = User.Create(request.Email, request.FirstName, request.LastName);

        var passwordHash = passwordHasher.HashPassword(user.Id, request.Password);

        user.UpdatePasswordHash(passwordHash);

        await writeRepository.AddAsync(user, cancellationToken);

        return new UserCreatedResponse(user.Id);
    }
}