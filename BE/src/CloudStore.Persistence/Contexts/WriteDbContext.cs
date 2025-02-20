using CloudStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Directory = CloudStore.Domain.Entities.Directory;
using File = CloudStore.Domain.Entities.File;

namespace CloudStore.Persistence.Contexts;

public class WriteDbContext(DbContextOptions<WriteDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Directory> Directories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly);
}