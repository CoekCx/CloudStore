using CloudStore.Domain.Abstractions.Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace CloudStore.Persistence.Interceptors;

public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities(eventData.Context!, DateTime.UtcNow);

        return new ValueTask<InterceptionResult<int>>(result);
    }

    private static void UpdateAuditableEntities(DbContext dbContext, DateTime utcNow)
    {
        foreach (EntityEntry<IAuditableEntity> entityEntry in dbContext.ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(IAuditableEntity.CreatedAt)).CurrentValue = utcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(IAuditableEntity.ModifiedAt)).CurrentValue = utcNow;
            }
        }
    }
}