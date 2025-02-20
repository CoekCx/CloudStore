using CloudStore.Application.Abstractions;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Directories;
using CloudStore.Domain.Repositories;
using CloudStore.Domain.Repositories.Directories;
using MediatR;
using Directory = CloudStore.Domain.Entities.Directory;
using DirectoryNotFoundException = CloudStore.Domain.Exceptions.Directories.DirectoryNotFoundException;

namespace CloudStore.Application.UseCases.Directories.Move;

public class MoveDirectoryCommandHandler(
    IDirectoryWriteRepository directoryWriteRepository,
    IDirectoryReadRepository directoryReadRepository,
    IFileSystemNameGenerator fileSystemNameGenerator,
    IUnitOfWork unitOfWork)
    : IRequestHandler<MoveDirectoryCommand, Unit>
{
    public async Task<Unit> Handle(MoveDirectoryCommand request, CancellationToken cancellationToken)
    {
        var directory = await directoryReadRepository.GetByIdAsync(new DirectoryId(request.DirectoryId), cancellationToken)
                        ?? throw new DirectoryNotFoundException(request.DirectoryId);

        if (directory.OwnerId != new UserId(request.OwnerId))
            throw new UnauthorizedDirectoryAccessException();

        if (directory.ParentDirectoryId == null)
            throw new RootDirectoryModificationException();

        if (directory.Id == new DirectoryId((Guid)request.NewParentDirectoryId!))
            throw new InvalidFolderMoveException("Cannot move directory into itself.");
        
        var newParentDirectoryId = new DirectoryId((Guid)request.NewParentDirectoryId!);
        var newParentDirectory =
            await directoryReadRepository.GetByIdAsync(newParentDirectoryId, cancellationToken);
        
        if (newParentDirectory == null)
            throw new DirectoryNotFoundException(newParentDirectoryId.Value);

        var uniqueName = await fileSystemNameGenerator.GenerateUniqueDirectoryName(directory.Name, newParentDirectoryId);
        directory.Name = uniqueName;
        directory.ParentDirectoryId = newParentDirectory!.Id;

        directoryWriteRepository.Update(directory);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}