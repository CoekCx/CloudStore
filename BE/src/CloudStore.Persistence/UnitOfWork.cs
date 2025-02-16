using CloudStore.Domain.Abstractions;
using CloudStore.Persistence.Context;

namespace CloudStore.Persistence;

public sealed class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
} 