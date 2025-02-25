using CloudStore.Application.Abstractions;
using CloudStore.Application.Responses.Directories;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Directories;
using CloudStore.Domain.Exceptions.Users;
using CloudStore.Domain.Repositories;
using MediatR;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Application.UseCases.Directories.Commands.Create;

public class CreateDirectoryCommandHandler(
    IUserRepository userRepository,
    IDirectoryRepository directoryRepository,
    IFileSystemNameGenerator fileSystemNameGenerator,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateDirectoryCommand, DirectoryResponse>
{
    public async Task<DirectoryResponse> Handle(CreateDirectoryCommand request, CancellationToken cancellationToken)
    {
        var owner = await userRepository.GetByIdAsync(new UserId(request.OwnerId), cancellationToken);

        if (owner == null)
        {
            throw new UserNotFoundException(request.OwnerId);
        }

        Directory? parentDirectory = null;
        if (request.ParentDirectoryId != null)
        {
            parentDirectory = await directoryRepository.GetByIdAsync(
                new DirectoryId((Guid)request.ParentDirectoryId),
                cancellationToken) ?? throw new RootDirectoryCreationException();
        }

        var uniqueName = await fileSystemNameGenerator.GenerateUniqueDirectoryNameAsync(request.Name, parentDirectory?.Id);
        var directory = Directory.Create(parentDirectory?.Id, uniqueName, owner.Id);

        directoryRepository.Add(directory, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return DirectoryResponse.FromDirectory(directory);
    }
}