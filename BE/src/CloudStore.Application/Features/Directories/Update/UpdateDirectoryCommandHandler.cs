using CloudStore.Application.Abstractions;
using CloudStore.Application.DTOs.Responses.Directories;
using CloudStore.Domain.Abstractions;
using CloudStore.Domain.Abstractions.Repositories.Directories;
using CloudStore.Domain.Exceptions.Directories;
using MediatR;
using DirectoryNotFoundException = CloudStore.Domain.Exceptions.Directories.DirectoryNotFoundException;

namespace CloudStore.Application.Features.Directories.Update;

public class UpdateDirectoryCommandHandler(
    IDirectoryWriteRepository directoryWriteRepository,
    IDirectoryReadRepository directoryReadRepository,
    IFileSystemNameGenerator fileSystemNameGenerator,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateDirectoryCommand, DirectoryResponse>
{
    public async Task<DirectoryResponse> Handle(UpdateDirectoryCommand request, CancellationToken cancellationToken)
    {
        var directory = await directoryReadRepository.GetByIdAsync(request.Id, cancellationToken)
                        ?? throw new DirectoryNotFoundException(request.Id);

        if (directory.OwnerId != request.OwnerId)
            throw new UnauthorizedDirectoryAccessException();

        if (directory.ParentDirectoryId == null)
            throw new RootDirectoryModificationException();

        var uniqueName =
            fileSystemNameGenerator.GenerateUniqueDirectoryName(request.NewName, directory.ParentDirectoryId);
        directory.Name = uniqueName;

        await directoryWriteRepository.UpdateAsync(directory, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return DirectoryResponse.FromDirectory(directory);
    }
}