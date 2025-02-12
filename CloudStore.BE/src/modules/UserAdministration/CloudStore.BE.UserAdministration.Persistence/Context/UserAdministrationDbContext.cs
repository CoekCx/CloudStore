using CloudStore.BE.UserAdministration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudStore.BE.UserAdministration.Persistence.Context;

public sealed class UserAdministrationDbContext(DbContextOptions<UserAdministrationDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserAdministrationDbContext).Assembly);
    }
}