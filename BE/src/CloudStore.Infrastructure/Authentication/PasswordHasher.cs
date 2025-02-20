using System.Text;
using CloudStore.Application.Abstractions;
using CloudStore.Domain.EntityIdentifiers;
using Epoche;

namespace CloudStore.Infrastructure.Authentication;

public sealed class PasswordHasher : IPasswordHasher
{
    public string HashPassword(UserId userId, string password)
    {
        var keccak = Keccak256.ComputeHash(Encoding.UTF8.GetBytes(userId + password));
        return BitConverter.ToString(keccak).Replace("-", "").ToLower();
    }

    public bool VerifyPassword(UserId userId, string password, string hashedPassword)
    {
        var computedHash = HashPassword(userId, password);
        return computedHash == hashedPassword;
    }
}