using CloudStore.Domain.Entities;
using CloudStore.Domain.EntityIdentifiers;

namespace CloudStore.Domain.Repositories.Users;

public interface IUserWriteRepository
{
    void Add(User user);

    void Remove(User user);

    Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken);
}