using CloudStore.Domain.Entities;

namespace CloudStore.Domain.Abstractions.Repositories.Base;

public interface IReadRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    IQueryable<TEntity> Query();
}