namespace UserAdministration.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public required string Email { get; init; }
    public string PasswordHash { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public bool IsEmailVerified { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static User Create(string email, string firstName, string lastName)
    {
        return new User()
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = "",
            FirstName = firstName,
            LastName = lastName,
            IsEmailVerified = false,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void VerifyEmail()
    {
        IsEmailVerified = true;
    }

    public void UpdatePasswordHash(string passwordHash)
    {
        if (!string.IsNullOrWhiteSpace(passwordHash))
        {
            PasswordHash = passwordHash;
        }
    }
}