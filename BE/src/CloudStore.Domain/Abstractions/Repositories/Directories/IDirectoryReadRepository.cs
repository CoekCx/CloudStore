using CloudStore.Domain.Abstractions.Repositories.Base;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Domain.Abstractions.Repositories.Directories;

public interface IDirectoryReadRepository : IReadRepository<Directory>
{
    bool DirectoryAlreadyExists(string name, Guid? parentId);

    Task<Directory?> GetByIdWithContentsAsync(Guid id, CancellationToken cancellationToken);

    Task<Directory> GetRootDirectory(Guid ownerId, CancellationToken cancellationToken);
}