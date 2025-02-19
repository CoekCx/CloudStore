using CloudStore.Domain.Abstractions.Core;

namespace CloudStore.Domain.EntityIdentifiers;

public record FileId(Guid Value) : IEntityId;