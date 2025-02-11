using CloudStore.BE.UserAdministration.Domain.Enums;
using Common.Constants;

namespace CloudStore.BE.UserAdministration.Domain.Entities;

public sealed class User
{
    // Private constructor for EF Core
    private User()
    {
    }

    public Guid Id { get; private set; }
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public bool IsEmailVerified { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public UserPlan Plan { get; private set; }
    public long StorageUsed { get; private set; } // In bytes
    public long StorageLimit { get; private set; } // In bytes

    // Factory method for creating new users
    public static User Create(
        string email,
        string passwordHash,
        string firstName,
        string lastName)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = passwordHash,
            FirstName = firstName,
            LastName = lastName,
            IsEmailVerified = false,
            CreatedAt = DateTime.UtcNow,
            Plan = UserPlan.Free,
            StorageUsed = 0,
            StorageLimit = StorageConstants.FreePlanStorageLimit
        };
    }

    public void Update(string firstName, string lastName, string email, string passwordHash)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;

        if (!string.IsNullOrWhiteSpace(passwordHash)) PasswordHash = passwordHash;
    }

    public void VerifyEmail()
    {
        IsEmailVerified = true;
    }

    public void UpdateStorageUsed(long newStorageUsed)
    {
        StorageUsed = newStorageUsed;
    }

    public void UpdateEmail(string email)
    {
        if (!string.IsNullOrWhiteSpace(email)) Email = email;
    }

    public void UpdatePassword(string passwordHash)
    {
        if (!string.IsNullOrWhiteSpace(passwordHash)) PasswordHash = passwordHash;
    }

    public void UpgradeToPremium()
    {
        Plan = UserPlan.Premium;
        StorageLimit = StorageConstants.PremiumPlanStorageLimit;
    }
}