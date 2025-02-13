using Microsoft.EntityFrameworkCore;
using UserAdministration.Domain.Entities;

namespace UserAdministration.Business.Abstractions;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}