using CloudStore.Domain.Abstractions.Repositories.Files;
using CloudStore.Persistence.Context;
using CloudStore.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using File = CloudStore.Domain.Entities.File;

namespace CloudStore.Persistence.Repositories.Files;

public class FileReadRepository(ReadOnlyApplicationDbContext context)
    : ReadRepository<File>(context), IFileReadRepository
{
    public Task<List<File>> GetByIds(List<Guid> ids)
    {
        return DbSet
            .Where(f => ids.Contains(f.Id))
            .ToListAsync();
    }

    public async Task<Guid> GetByDirectoryId(Guid directoryId)
    {
        var file = await DbSet
            .FirstOrDefaultAsync(f => f.ParentDirectory.Id == directoryId);

        return file?.Id ?? Guid.Empty;
    }
}