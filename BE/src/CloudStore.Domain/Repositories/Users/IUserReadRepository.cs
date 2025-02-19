using CloudStore.Domain.Entities;
using CloudStore.Domain.EntityIdentifiers;

namespace CloudStore.Domain.Repositories.Users;

public interface IUserReadRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);

    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken);

    Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
}