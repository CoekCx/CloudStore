using CloudStore.Domain.Abstractions.Core;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Events.Users;

namespace CloudStore.Domain.Entities;

public class User : Entity<UserId>, IAuditableEntity
{
    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public bool IsEmailVerified { get; private set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    private User(
        UserId id,
        string email,
        string passwordHash,
        string firstName,
        string lastName,
        bool isEmailVerified,
        DateTime createdAt) : base(id)
    {
        Email = email;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        IsEmailVerified = isEmailVerified;
        CreatedAt = createdAt;

        RaiseDomainEvent(new UserCreatedDomainEvent(id.Value));
    }

    public static User Create(string email, string firstName, string lastName) =>
        new(
            new UserId(Guid.NewGuid()),
            email,
            "",
            firstName,
            lastName,
            false,
            DateTime.UtcNow);

    public void VerifyEmail() => IsEmailVerified = true;

    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public void UpdatePasswordHash(string passwordHash)
    {
        if (!string.IsNullOrWhiteSpace(passwordHash))
        {
            PasswordHash = passwordHash;
        }
    }
}