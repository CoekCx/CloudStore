using CloudStore.Domain.Entities;
using CloudStore.Domain.Repositories.Users;
using CloudStore.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using CloudStore.Domain.EntityIdentifiers;

namespace CloudStore.Persistence.Repositories.Users;

public class UserReadRepository(ReadDbContext context) : IUserReadRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken) =>
        await context.Set<User>().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken) =>
        await context.Set<User>().AnyAsync(x => x.Email == email, cancellationToken);

    public async Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken) =>
        await context.Set<User>().FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken) =>
        await context.Set<User>().ToListAsync(cancellationToken);
}