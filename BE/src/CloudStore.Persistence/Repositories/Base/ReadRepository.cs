using CloudStore.Domain.Abstractions.Repositories.Base;
using CloudStore.Domain.Entities;
using CloudStore.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CloudStore.Persistence.Repositories.Base;

public class ReadRepository<TEntity>(ReadOnlyApplicationDbContext context) : IReadRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public virtual IQueryable<TEntity> Query()
    {
        return DbSet.AsQueryable();
    }
}