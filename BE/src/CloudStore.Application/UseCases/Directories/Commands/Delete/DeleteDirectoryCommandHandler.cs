using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Directories;
using CloudStore.Domain.Repositories;
using MediatR;
using DirectoryNotFoundException = CloudStore.Domain.Exceptions.Directories.DirectoryNotFoundException;

namespace CloudStore.Application.UseCases.Directories.Commands.Delete;

public class DeleteDirectoryCommandHandler(
    IDirectoryRepository directoryRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteDirectoryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteDirectoryCommand request, CancellationToken cancellationToken)
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

        directoryRepository.Delete(directory, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}