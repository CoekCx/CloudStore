using System.Text;
using CloudStore.Application.Abstractions;
using Epoche;

namespace CloudStore.Infrastructure.Services;

public sealed class PasswordHasher : IPasswordHasher
{
    public string HashPassword(Guid userId, string password)
    {
        var keccak = Keccak256.ComputeHash(Encoding.UTF8.GetBytes(userId + password));
        return BitConverter.ToString(keccak).Replace("-", "").ToLower();
    }

    public bool VerifyPassword(Guid userId, string password, string hashedPassword)
    {
        var computedHash = HashPassword(userId, password);
        return computedHash == hashedPassword;
    }
}