using CloudStore.Domain.Entities;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CloudStore.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public void Add(User user) =>
        context.Users.Add(user);

    public void Delete(User user) =>
        context.Users.Remove(user);

    public Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken) =>
        context.Users.FirstOrDefaultAsync(x => x!.Id == userId, cancellationToken);

    public Task<User> GetByEmailAsync(string requestEmail, CancellationToken cancellationToken) =>
        context.Users.FirstOrDefaultAsync(x => x!.Email == requestEmail, cancellationToken)!;

    public Task<bool> EmailExistsAsync(string requestEmail, CancellationToken cancellationToken) =>
        context.Users.AnyAsync(x => x!.Email == requestEmail, cancellationToken);
}