using CloudStore.Application.Abstractions;
using CloudStore.Application.Responses.Directories;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Directories;
using CloudStore.Domain.Repositories;
using MediatR;
using DirectoryNotFoundException = CloudStore.Domain.Exceptions.Directories.DirectoryNotFoundException;

namespace CloudStore.Application.UseCases.Directories.Commands.Update;

public class UpdateDirectoryCommandHandler(
    IDirectoryRepository directoryRepository,
    IFileSystemNameGenerator fileSystemNameGenerator,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateDirectoryCommand, DirectoryResponse>
{
    public async Task<DirectoryResponse> Handle(UpdateDirectoryCommand request, CancellationToken cancellationToken)
    {
        var directory = await directoryRepository.GetByIdAsync(new DirectoryId(request.Id), cancellationToken)
                        ?? throw new DirectoryNotFoundException(request.Id);

        if (directory.OwnerId != new UserId(request.OwnerId))
        {
            throw new UnauthorizedDirectoryAccessException();
        }

        if (directory.ParentDirectoryId == null)
        {
            throw new RootDirectoryModificationException();
        }
        
        if (directory.Name == request.NewName)
        {
            return DirectoryResponse.FromDirectory(directory);
        }

        var uniqueName = await fileSystemNameGenerator.GenerateUniqueDirectoryNameAsync(
            request.NewName,
            directory.ParentDirectoryId,
            directory.Name);
        directory.Name = uniqueName;

        directoryRepository.Update(directory);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return DirectoryResponse.FromDirectory(directory);
    }
}