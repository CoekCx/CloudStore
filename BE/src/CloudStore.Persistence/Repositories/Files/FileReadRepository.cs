using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Repositories.Files;
using CloudStore.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using File = CloudStore.Domain.Entities.File;

namespace CloudStore.Persistence.Repositories.Files;

public class FileReadRepository(ReadDbContext context) : IFileReadRepository
{
    // Maybe needs to be with value
    public async Task<List<File>> GetByIds(List<FileId> ids, CancellationToken cancellationToken) => 
        await context.Set<File>()
            .Where(x => ids.Select(x=>x).Contains(x.Id))
            .ToListAsync(cancellationToken);

    public async Task<File?> GetById(FileId id, CancellationToken cancellationToken) =>
        await context.Set<File>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
}