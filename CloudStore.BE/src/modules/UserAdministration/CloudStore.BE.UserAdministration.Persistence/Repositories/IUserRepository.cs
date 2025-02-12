using CloudStore.BE.UserAdministration.Domain.Entities;
using Common.Models;

namespace CloudStore.BE.UserAdministration.Persistence.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<PaginatedList<User>> GetUsersAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default);

    Task AddAsync(User user, CancellationToken cancellationToken = default);

    void Update(User user);

    void Remove(User user);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}