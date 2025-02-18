using CloudStore.Application.Abstractions;
using CloudStore.Domain.Abstractions;
using CloudStore.Domain.Abstractions.Repositories.Directories;
using CloudStore.Domain.Exceptions.Directories;
using MediatR;
using DirectoryNotFoundException = CloudStore.Domain.Exceptions.Directories.DirectoryNotFoundException;

namespace CloudStore.Application.Features.Directories.Move;

public class MoveDirectoryCommandHandler(
    IDirectoryWriteRepository directoryWriteRepository,
    IDirectoryReadRepository directoryReadRepository,
    IFileSystemNameGenerator fileSystemNameGenerator,
    IUnitOfWork unitOfWork)
    : IRequestHandler<MoveDirectoryCommand, Unit>
{
    public async Task<Unit> Handle(MoveDirectoryCommand request, CancellationToken cancellationToken)
    {
        var directory = await directoryReadRepository.GetByIdAsync(request.DirectoryId, cancellationToken)
                        ?? throw new DirectoryNotFoundException(request.DirectoryId);

        if (directory.OwnerId != request.OwnerId)
            throw new UnauthorizedDirectoryAccessException();

        if (directory.ParentDirectoryId == null)
            throw new RootDirectoryModificationException();

        if (directory.Id == request.NewParentDirectoryId)
            throw new InvalidFolderMoveException("Cannot move directory into itself.");

        var newParentDirectory =
            await directoryReadRepository.GetByIdAsync((Guid)request.NewParentDirectoryId!, cancellationToken);

        var uniqueName = fileSystemNameGenerator.GenerateUniqueDirectoryName(directory.Name, request.NewParentDirectoryId);
        directory.Name = uniqueName;
        directory.ParentDirectoryId = newParentDirectory!.Id;

        await directoryWriteRepository.UpdateAsync(directory, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}