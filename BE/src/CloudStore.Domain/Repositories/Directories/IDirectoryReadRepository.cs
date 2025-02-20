using CloudStore.Domain.EntityIdentifiers;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Domain.Repositories.Directories;

public interface IDirectoryReadRepository
{
    Task<bool> ExistsAsync(string name, DirectoryId? parentId);

    Task<Directory?> GetByIdWithContentsAsync(DirectoryId id, CancellationToken cancellationToken);

    Task<Directory?> GetRootDirectory(UserId ownerId, CancellationToken cancellationToken);
    
    Task<Directory?> GetByIdAsync(DirectoryId directoryId, CancellationToken cancellationToken);
}