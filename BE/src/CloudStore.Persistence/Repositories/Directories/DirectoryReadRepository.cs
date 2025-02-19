using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Directories;
using CloudStore.Domain.Repositories.Directories;
using CloudStore.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Persistence.Repositories.Directories;

public class DirectoryReadRepository(ReadDbContext context) : IDirectoryReadRepository
{
    public Task<bool> ExistsAsync(string name, DirectoryId? parentId)
    {
        throw new NotImplementedException();
    }

    public Task<Directory?> GetByIdWithContentsAsync(DirectoryId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Directory> GetRootDirectory(UserId ownerId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}