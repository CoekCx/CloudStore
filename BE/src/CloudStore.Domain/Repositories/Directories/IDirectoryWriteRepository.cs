using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Domain.Repositories.Directories;

public interface IDirectoryWriteRepository
{
    void Add(Directory directory, CancellationToken cancellationToken);
    
    void Delete(Directory directory, CancellationToken cancellationToken);
    
    public void Update(Directory directory);
}