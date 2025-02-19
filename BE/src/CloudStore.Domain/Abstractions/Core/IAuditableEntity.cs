namespace CloudStore.Domain.Abstractions.Core;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }

    DateTime? ModifiedAt { get; set; }
}