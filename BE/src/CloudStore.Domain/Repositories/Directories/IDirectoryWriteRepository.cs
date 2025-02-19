using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Domain.Repositories.Directories;

public interface IDirectoryWriteRepository
{
    void Add(Directory directory, CancellationToken cancellationToken);
}