using CloudStore.Domain.Abstractions.Core;

namespace CloudStore.Domain.EntityIdentifiers;

public record DirectoryId(Guid Value) : IEntityId;