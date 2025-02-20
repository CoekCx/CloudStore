using CloudStore.Application.Abstractions;
using CloudStore.Application.Responses.Directories;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Directories;
using CloudStore.Domain.Exceptions.Users;
using CloudStore.Domain.Repositories;
using CloudStore.Domain.Repositories.Directories;
using CloudStore.Domain.Repositories.Users;
using MediatR;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Application.UseCases.Directories.Create;

public class CreateDirectoryCommandHandler(
    IDirectoryWriteRepository directoryWriteRepository,
    IDirectoryReadRepository directoryReadRepository,
    IUserReadRepository userReadRepository,
    IFileSystemNameGenerator fileSystemNameGenerator,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateDirectoryCommand, DirectoryResponse>
{
    public async Task<DirectoryResponse> Handle(CreateDirectoryCommand request, CancellationToken cancellationToken)
    {
        var owner = await userReadRepository.GetByIdAsync(new UserId(request.OwnerId), cancellationToken)
                    ?? throw new UserNotFoundException(request.OwnerId);

        Directory? parentDirectory = null;
        if (request.ParentDirectoryId != null)
            parentDirectory = await directoryReadRepository.GetByIdAsync(
                new DirectoryId((Guid)request.ParentDirectoryId),
                cancellationToken) ?? throw new RootDirectoryCreationException();

        var uniqueName = await fileSystemNameGenerator.GenerateUniqueDirectoryName(request.Name, parentDirectory?.Id);
        var directory = Directory.Create(parentDirectory?.Id, uniqueName, owner.Id);

        directoryWriteRepository.Add(directory, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return DirectoryResponse.FromDirectory(directory);
    }
}