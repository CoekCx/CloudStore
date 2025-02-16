using CloudStore.Domain.Abstractions.Repositories.Base;
using CloudStore.Domain.Entities;

namespace CloudStore.Domain.Abstractions.Repositories.Users;

public interface IUserReadRepository : IReadRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
}