using CloudStore.Application.Responses.Directories;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Persistence.Repositories;

public class DirectoryRepository(ApplicationDbContext context) : IDirectoryRepository
{
    public void Add(Directory directory, CancellationToken cancellationToken) =>
        context.Directories.Add(directory);

    public void Delete(Directory directory, CancellationToken cancellationToken) =>
        context.Directories.Remove(directory);

    public void Update(Directory directory) =>
        context.Directories.Update(directory);

    public Task<Directory?> GetByIdAsync(DirectoryId directoryId, CancellationToken cancellationToken) =>
        context.Directories.FirstOrDefaultAsync(x => x.Id == directoryId, cancellationToken);

    public Task<Directory> GetByIdWithContentsAsync(DirectoryId id, CancellationToken cancellationToken) =>
        context.Directories
            .Include(x => x.Subdirectories)
            .Include(x => x.Files)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)!;

    public Task<bool> ExistsAsync(string newName, DirectoryId? parentId, CancellationToken cancellationToken) =>
        context.Directories.AnyAsync(x => x.ParentDirectoryId == parentId && x.Name == newName, cancellationToken);
}