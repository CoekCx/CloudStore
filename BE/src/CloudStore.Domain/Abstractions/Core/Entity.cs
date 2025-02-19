namespace CloudStore.Domain.Abstractions.Core;

public abstract class Entity<TEntityId> where TEntityId : class, IEntityId
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public TEntityId Id { get; private set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.ToList();

    protected Entity(TEntityId id) => Id = id;

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void ClearDomainEvents<T>() => _domainEvents.OfType<T>()
        .ToList()
        .Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}