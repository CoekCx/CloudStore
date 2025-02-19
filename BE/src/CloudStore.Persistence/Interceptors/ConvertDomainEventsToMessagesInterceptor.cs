using CloudStore.Domain.Abstractions.Core;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CloudStore.Persistence.Outbox;

namespace CloudStore.Persistence.Interceptors;

public sealed class ConvertDomainEventsToMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ConvertDomainEventsToMessages(eventData.Context!);

        return new ValueTask<InterceptionResult<int>>(result);
    }

    private static void ConvertDomainEventsToMessages(DbContext dbContext)
    {
        var entities = dbContext.ChangeTracker
            .Entries<Entity<IEntityId>>()
            .Select(entityEntry => entityEntry.Entity)
            .Where(entities => entities.DomainEvents.Any())
            .ToList();

        foreach (var entity in entities)
        {
            foreach (var domainEvent in entity.DomainEvents)
            {
                var content = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                var outboxMessage = new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    Type = domainEvent.GetType().FullName!,
                    Content = content,
                    CreatedAt = DateTime.UtcNow
                };

                dbContext.Set<OutboxMessage>().Add(outboxMessage);
            }

            entity.ClearDomainEvents();
        }
    }
}