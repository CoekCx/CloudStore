using CloudStore.Domain.Abstractions.Core;

namespace CloudStore.Domain.Events.Users;

public sealed record UserCreatedDomainEvent(Guid Id) : DomainEvent(Guid.NewGuid());
