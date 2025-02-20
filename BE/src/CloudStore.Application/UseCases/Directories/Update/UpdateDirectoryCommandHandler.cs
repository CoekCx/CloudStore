using CloudStore.Application.Abstractions;
using CloudStore.Application.Responses.Directories;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Directories;
using CloudStore.Domain.Repositories;
using CloudStore.Domain.Repositories.Directories;
using MediatR;
using Directory = CloudStore.Domain.Entities.Directory;
using DirectoryNotFoundException = CloudStore.Domain.Exceptions.Directories.DirectoryNotFoundException;

namespace CloudStore.Application.UseCases.Directories.Update;

public class UpdateDirectoryCommandHandler(
    IDirectoryWriteRepository directoryWriteRepository,
    IDirectoryReadRepository directoryReadRepository,
    IFileSystemNameGenerator fileSystemNameGenerator,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateDirectoryCommand, DirectoryResponse>
{
    public async Task<DirectoryResponse> Handle(UpdateDirectoryCommand request, CancellationToken cancellationToken)
    {
        var directory = await directoryReadRepository.GetByIdAsync(new DirectoryId(request.Id), cancellationToken)
                        ?? throw new DirectoryNotFoundException(request.Id);

        if (directory.OwnerId != new UserId(request.OwnerId))
            throw new UnauthorizedDirectoryAccessException();

        if (directory.ParentDirectoryId == null)
            throw new RootDirectoryModificationException();

        var uniqueName =
            await fileSystemNameGenerator.GenerateUniqueDirectoryName(request.NewName, directory.ParentDirectoryId);
        directory.Name = uniqueName;

        directoryWriteRepository.Update(directory);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return DirectoryResponse.FromDirectory(directory);
    }
}