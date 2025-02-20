using CloudStore.Application.Abstractions;
using CloudStore.Application.Responses.Users;
using CloudStore.Domain.Entities;
using CloudStore.Domain.Exceptions.Users;
using CloudStore.Domain.Repositories;
using CloudStore.Domain.Repositories.Directories;
using CloudStore.Domain.Repositories.Users;
using MediatR;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Application.UseCases.Users.Create;

public sealed class CreateUserCommandHandler(
    IUserWriteRepository writeRepository,
    IUserReadRepository readRepository,
    IDirectoryWriteRepository directoryWriteRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await readRepository.EmailExistsAsync(request.Email, cancellationToken))
        {
            throw new UserEmailAlreadyExists(request.Email);
        }

        var user = User.Create(request.Email, request.FirstName, request.LastName);

        user.UpdatePasswordHash(passwordHasher.HashPassword(user.Id, request.Password));

        writeRepository.Add(user);
        
        var rootDirectory = Directory.Create(null, "Root", user.Id);
        directoryWriteRepository.Add(rootDirectory, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id.Value;
    }
}