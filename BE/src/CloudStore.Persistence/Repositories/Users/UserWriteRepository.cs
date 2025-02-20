using CloudStore.Domain.Entities;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Repositories.Users;
using CloudStore.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CloudStore.Persistence.Repositories.Users;

public class UserWriteRepository(WriteDbContext context) : IUserWriteRepository
{
    public void Add(User user) =>
        context.Set<User>().Add(user);

    public void Remove(User user) =>
        context.Set<User>().Remove(user);

    public Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken) =>
        context.Set<User>().FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
}