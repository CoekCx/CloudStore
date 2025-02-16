using CloudStore.Domain.Abstractions.Repositories.Users;
using CloudStore.Domain.Entities;
using CloudStore.Domain.Exceptions.Users;
using CloudStore.Persistence.Context;
using CloudStore.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CloudStore.Persistence.Repositories.Users;

public class UserWriteRepository(ApplicationDbContext context) : WriteRepository<User>(context), IUserWriteRepository
{
    public async Task UpdatePasswordHashAsync(Guid userId, string newPasswordHash, CancellationToken cancellationToken)
    {
        var user = await DbSet.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user == null) throw new UserNotFoundException(userId);

        user.PasswordHash = newPasswordHash;
        await UpdateAsync(user, cancellationToken);
    }

    public async Task UpdateAsync(
        Guid userId,
        string firstName,
        string lastName,
        CancellationToken cancellationToken)
    {
        var user = await DbSet.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user == null) throw new UserNotFoundException(userId);

        user.FirstName = firstName;
        user.LastName = lastName;
        await UpdateAsync(user, cancellationToken);
    }
}