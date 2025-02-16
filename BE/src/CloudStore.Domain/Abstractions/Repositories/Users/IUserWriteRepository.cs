using CloudStore.Domain.Abstractions.Repositories.Base;
using CloudStore.Domain.Entities;

namespace CloudStore.Domain.Abstractions.Repositories.Users;

public interface IUserWriteRepository : IWriteRepository<User>
{
    Task UpdatePasswordHashAsync(Guid userId, string newPasswordHash, CancellationToken cancellationToken);
    Task UpdateAsync(Guid userId, string firstName, string lastName, CancellationToken cancellationToken);
}