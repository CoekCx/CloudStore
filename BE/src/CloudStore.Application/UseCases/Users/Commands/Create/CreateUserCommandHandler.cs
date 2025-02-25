using CloudStore.Application.Abstractions;
using CloudStore.Domain.Entities;
using CloudStore.Domain.Exceptions.Users;
using CloudStore.Domain.Repositories;
using MediatR;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Application.UseCases.Users.Commands.Create;

public sealed class CreateUserCommandHandler(
    IUserRepository repository,
    IDirectoryRepository directoryRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await repository.EmailExistsAsync(request.Email, cancellationToken))
        {
            throw new UserEmailAlreadyExists(request.Email);
        }

        var user = User.Create(request.Email, request.FirstName, request.LastName);

        user.UpdatePasswordHash(passwordHasher.HashPassword(user.Id, request.Password));

        repository.Add(user);
        
        var rootDirectory = Directory.Create(null, "Root", user.Id);
        directoryRepository.Add(rootDirectory, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id.Value;
    }
}