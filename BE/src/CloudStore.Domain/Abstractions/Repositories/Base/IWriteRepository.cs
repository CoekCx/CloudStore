using CloudStore.Domain.Entities;

namespace CloudStore.Domain.Abstractions.Repositories.Base;

public interface IWriteRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
}