using CloudStore.Domain.Entities;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Repositories.Users;
using CloudStore.Persistence.Contexts;

namespace CloudStore.Persistence.Repositories.Users;

public class UserWriteRepository(WriteDbContext context) : IUserWriteRepository
{
    public void Add(User user)
    {
        throw new NotImplementedException();
    }

    public void Remove(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(UserId userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}