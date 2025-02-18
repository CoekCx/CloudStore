using CloudStore.Domain.Abstractions.Repositories.Directories;
using CloudStore.Domain.Exceptions.Directories;
using CloudStore.Persistence.Context;
using CloudStore.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Persistence.Repositories.Directories;

public class DirectoryReadRepository(ReadOnlyApplicationDbContext context)
    : ReadRepository<Directory>(context), IDirectoryReadRepository
{
    public override Task<Directory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return DbSet.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public bool DirectoryAlreadyExists(string name, Guid? parentId)
    {
        return DbSet.Any(d => d.Name == name && d.ParentDirectoryId == parentId);
    }

    public Task<Directory?> GetByIdWithContentsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return DbSet
            .Include(d => d.Subdirectories)
            .Include(d => d.Files)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public Task<Directory> GetRootDirectory(Guid ownerId, CancellationToken cancellationToken = default)
    {
        return (DbSet
                    .FirstOrDefaultAsync(d => d.ParentDirectoryId == null && d.OwnerId == ownerId, cancellationToken)
                ?? throw new RootDirectoryNotFoundException(ownerId))!;
    }
}