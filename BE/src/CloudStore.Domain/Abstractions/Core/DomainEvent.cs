namespace CloudStore.Domain.Abstractions.Core;

public abstract record DomainEvent(Guid Id) : IDomainEvent;