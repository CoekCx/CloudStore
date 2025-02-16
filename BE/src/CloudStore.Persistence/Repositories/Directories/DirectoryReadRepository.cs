using CloudStore.Domain.Abstractions.Repositories.Directories;
using CloudStore.Persistence.Context;
using CloudStore.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Persistence.Repositories.Directories;

public class DirectoryReadRepository(ReadOnlyApplicationDbContext context)
    : ReadRepository<Directory>(context), IDirectoryReadRepository
{
    public Task<List<Directory>> GetChildDirectories(Guid id)
    {
        return DbSet
            .Where(d => d.ParentDirectory != null && d.ParentDirectory.Id == id)
            .ToListAsync();
    }
}