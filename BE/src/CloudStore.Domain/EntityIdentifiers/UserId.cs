using CloudStore.Domain.Abstractions.Core;

namespace CloudStore.Domain.EntityIdentifiers;

public record UserId(Guid Value) : IEntityId;