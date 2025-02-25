using CloudStore.Domain.Entities;
using CloudStore.Domain.EntityIdentifiers;

namespace CloudStore.Domain.Repositories;

public interface IUserRepository
{
    void Add(User user);

    void Delete(User user);

    Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken);
    
    Task<User> GetByEmailAsync(string requestEmail, CancellationToken cancellationToken);
    
    Task<bool> EmailExistsAsync(string requestEmail, CancellationToken cancellationToken);
}