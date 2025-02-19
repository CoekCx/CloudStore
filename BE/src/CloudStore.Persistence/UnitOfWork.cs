using CloudStore.Domain.Repositories;
using CloudStore.Persistence.Contexts;

namespace CloudStore.Persistence;

public sealed class UnitOfWork(WriteDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
} 