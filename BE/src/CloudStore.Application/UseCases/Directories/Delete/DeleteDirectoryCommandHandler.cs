using CloudStore.Domain.Exceptions.Directories;
using CloudStore.Domain.Repositories;
using CloudStore.Domain.Repositories.Directories;
using MediatR;
using DirectoryNotFoundException = CloudStore.Domain.Exceptions.Directories.DirectoryNotFoundException;

namespace CloudStore.Application.UseCases.Directories.Delete;

public class DeleteDirectoryCommandHandler(
    IDirectoryWriteRepository directoryWriteRepository,
    IDirectoryReadRepository directoryReadRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteDirectoryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteDirectoryCommand request, CancellationToken cancellationToken)
    {
        var directory = await directoryReadRepository.GetByIdAsync(request.DirectoryId, cancellationToken)
                        ?? throw new DirectoryNotFoundException(request.DirectoryId);

        if (directory.OwnerId != request.OwnerId)
            throw new UnauthorizedDirectoryAccessException();

        if (directory.ParentDirectoryId == null)
            throw new RootDirectoryModificationException();

        await directoryWriteRepository.DeleteAsync(directory, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}