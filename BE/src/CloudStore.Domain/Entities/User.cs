namespace CloudStore.Domain.Entities;

public class User : BaseEntity
{
    public required string Email { get; init; }
    public string PasswordHash { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public bool IsEmailVerified { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static User Create(string email, string firstName, string lastName)
    {
        return new User
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

    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public void UpdatePasswordHash(string passwordHash)
    {
        if (!string.IsNullOrWhiteSpace(passwordHash)) PasswordHash = passwordHash;
    }
}