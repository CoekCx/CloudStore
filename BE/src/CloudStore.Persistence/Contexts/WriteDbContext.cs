using Microsoft.EntityFrameworkCore;

namespace CloudStore.Persistence.Contexts;

public class WriteDbContext(DbContextOptions<WriteDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly);
}