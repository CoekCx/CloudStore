using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserAdministration.Business.Abstractions;
using UserAdministration.Domain.Entities;

namespace UserAdministration.Persistence;

public class UserAdministrationDbContext(DbContextOptions options)
    : DbContext(options), IApplicationDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    public DbSet<User> Users { get; set; }
}