using CloudStore.Domain.Abstractions.Repositories.Users;
using CloudStore.Domain.Entities;
using CloudStore.Persistence.Context;
using CloudStore.Persistence.Repositories.Base;

namespace CloudStore.Persistence.Repositories.Users;

public class UserReadRepository(ReadOnlyApplicationDbContext context)
    : ReadRepository<User>(context), IUserReadRepository
{
    public Task<User?> GetByEmailAsync(string email)
    {
        return Task.FromResult(DbSet.FirstOrDefault(x => x.Email == email));
    }

    public Task<bool> EmailExistsAsync(string email)
    {
        return Task.FromResult(DbSet.Any(x => x.Email == email));
    }
}