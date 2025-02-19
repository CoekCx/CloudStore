using Microsoft.EntityFrameworkCore;

namespace CloudStore.Persistence.Contexts;

public sealed class ReadDbContext : WriteDbContext
{
    public ReadDbContext(DbContextOptions<WriteDbContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public override int SaveChanges()
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }
}