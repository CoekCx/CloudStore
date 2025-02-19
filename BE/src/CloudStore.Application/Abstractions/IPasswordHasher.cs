using CloudStore.Domain.EntityIdentifiers;

namespace CloudStore.Application.Abstractions;

public interface IPasswordHasher
{
    string HashPassword(UserId id, string password);

    bool VerifyPassword(UserId id, string password, string hashedPassword);
}