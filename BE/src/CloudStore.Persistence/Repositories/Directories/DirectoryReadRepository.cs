using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Directories;
using CloudStore.Domain.Repositories.Directories;
using CloudStore.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Persistence.Repositories.Directories;

public class DirectoryReadRepository(ReadDbContext context) : IDirectoryReadRepository
{
    public async Task<bool> ExistsAsync(string name, DirectoryId? parentId) =>
        await context.Set<Directory>().AnyAsync(x => x.Name == name);

    public async Task<Directory?> GetByIdWithContentsAsync(DirectoryId id, CancellationToken cancellationToken) =>
        await context.Set<Directory>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<Directory?> GetRootDirectory(UserId ownerId, CancellationToken cancellationToken) =>
        await context.Set<Directory>().FirstOrDefaultAsync(
            x => x.OwnerId == ownerId && x.ParentDirectoryId == null,
            cancellationToken);

    public async Task<Directory?> GetByIdAsync(DirectoryId directoryId, CancellationToken cancellationToken) =>
        await context.Set<Directory>().FirstOrDefaultAsync(x => x.Id == directoryId, cancellationToken);
}