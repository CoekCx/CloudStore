using CloudStore.Application.Abstractions;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Directories;
using CloudStore.Domain.Repositories;
using MediatR;
using DirectoryNotFoundException = CloudStore.Domain.Exceptions.Directories.DirectoryNotFoundException;

namespace CloudStore.Application.UseCases.Directories.Commands.Move;

public class MoveDirectoryCommandHandler(
    IDirectoryRepository directoryRepository,
    IFileSystemNameGenerator fileSystemNameGenerator,
    IUnitOfWork unitOfWork)
    : IRequestHandler<MoveDirectoryCommand, Unit>
{
    public async Task<Unit> Handle(MoveDirectoryCommand request, CancellationToken cancellationToken)
    {
        var directory = await directoryRepository.GetByIdAsync(new DirectoryId(request.DirectoryId), cancellationToken);

        if (directory == null)
        {
            throw new DirectoryNotFoundException(request.DirectoryId);
        }

        if (directory.OwnerId != new UserId(request.OwnerId))
        {
            throw new UnauthorizedDirectoryAccessException();
        }

        if (directory.ParentDirectoryId == null)
        {
            throw new RootDirectoryModificationException();
        }

        if (directory.Id == new DirectoryId((Guid)request.NewParentDirectoryId!))
        {
            throw new InvalidFolderMoveException("Cannot move directory into itself.");
        }

        if (directory.ParentDirectoryId == new DirectoryId((Guid)request.NewParentDirectoryId))
        {
            return Unit.Value;
        }
        
        var newParentDirectoryId = new DirectoryId((Guid)request.NewParentDirectoryId!);
        var newParentDirectory =
            await directoryRepository.GetByIdAsync(newParentDirectoryId, cancellationToken);
        
        if (newParentDirectory == null)
        {
            throw new DirectoryNotFoundException(newParentDirectoryId.Value);
        }

        var uniqueName = await fileSystemNameGenerator.GenerateUniqueDirectoryNameAsync(directory.Name, newParentDirectoryId);
        directory.Name = uniqueName;
        directory.ParentDirectoryId = newParentDirectory!.Id;

        directoryRepository.Update(directory);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}