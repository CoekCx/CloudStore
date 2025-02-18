using CloudStore.Application.Abstractions;
using CloudStore.Application.Responses.Users;
using CloudStore.Domain.Abstractions;
using CloudStore.Domain.Abstractions.Repositories.Directories;
using CloudStore.Domain.Abstractions.Repositories.Users;
using CloudStore.Domain.Entities;
using CloudStore.Domain.Exceptions.Users;
using MediatR;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Application.UseCases.Users.Create;

public sealed class CreateUserCommandHandler(
    IUserWriteRepository writeRepository,
    IUserReadRepository readRepository,
    IDirectoryWriteRepository directoryWriteRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateUserCommand, UserCreatedResponse>
{
    public async Task<UserCreatedResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var emailExists = await readRepository.EmailExistsAsync(request.Email);
        if (emailExists) throw new UserEmailAlreadyExists(request.Email);

        // Create user
        var user = User.Create(request.Email, request.FirstName, request.LastName);
        var passwordHash = passwordHasher.HashPassword(user.Id, request.Password);
        user.UpdatePasswordHash(passwordHash);
        await writeRepository.AddAsync(user, cancellationToken);

        // Create root directory
        var rootDirectory = new Directory(null, "Root", user.Id);
        await directoryWriteRepository.AddAsync(rootDirectory, cancellationToken);

        // Save all changes atomically
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UserCreatedResponse(user.Id);
    }
}