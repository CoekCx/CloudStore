using CloudStore.Domain.Repositories.Directories;
using CloudStore.Persistence.Contexts;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Persistence.Repositories.Directories;

public class DirectoryWriteRepository(WriteDbContext context) : IDirectoryWriteRepository
{
    public void Add(Directory directory, CancellationToken cancellationToken)
    {
        context.Set<Directory>().Add(directory);
    }
}