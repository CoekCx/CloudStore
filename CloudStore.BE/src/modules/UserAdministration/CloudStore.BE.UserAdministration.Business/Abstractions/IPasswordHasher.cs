namespace CloudStore.BE.UserAdministration.Business.Abstractions;

public interface IPasswordHasher
{
    string HashPassword(Guid userId, string password);
    bool VerifyPassword(Guid userId, string password, string hashedPassword);
}