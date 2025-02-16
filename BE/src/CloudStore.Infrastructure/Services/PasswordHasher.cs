using System.Text;
using CloudStore.Application.Abstractions;
using Epoche;

namespace CloudStore.Infrastructure.Services;

public sealed class PasswordHasher : IPasswordHasher
{
    public string HashPassword(Guid userId, string password)
    {
        var keccak = Keccak256.ComputeHash(Encoding.UTF8.GetBytes(userId + password));

        return Encoding.UTF8.GetString(keccak);
    }

    public bool VerifyPassword(Guid userId, string password, string hashedPassword)
    {
        var computedHash = HashPassword(userId, password);
        return computedHash == hashedPassword;
    }
}