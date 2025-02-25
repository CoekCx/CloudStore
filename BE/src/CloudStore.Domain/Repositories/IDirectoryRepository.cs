using CloudStore.Domain.EntityIdentifiers;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Domain.Repositories;

public interface IDirectoryRepository
{
    void Add(Directory directory, CancellationToken cancellationToken);
    
    void Delete(Directory directory, CancellationToken cancellationToken);
    
    public void Update(Directory directory);
    
    Task<Directory?> GetByIdAsync(DirectoryId directoryId, CancellationToken cancellationToken);
    
    Task<Directory> GetByIdWithContentsAsync(DirectoryId id, CancellationToken cancellationToken);
    
    Task<bool> ExistsAsync(string newName, DirectoryId? parentId, CancellationToken cancellationToken);
}