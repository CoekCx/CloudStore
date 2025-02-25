using System.Text.Json;
using CloudStore.Persistence;
using CloudStore.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace CloudStore.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class ProcessOutboxJob(ApplicationDbContext dbContext, IPublisher publisher) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await GetUnprocessedMessages(context.CancellationToken);

        if (!messages.Any())
        {
            return;
        }

        foreach (var message in messages)
        {
            try
            {
                var type = typeof(Domain.AssemblyReference).Assembly.GetType(message.Type)!;
                var deserializedEvent = JsonSerializer.Deserialize(message.Content, type)!;

                await publisher.Publish(deserializedEvent, context.CancellationToken);

                message.ProcessedAt = DateTime.UtcNow;
            }
            catch (Exception exception)
            {
                message.Error = exception.Message;
            }
        }

        await dbContext.SaveChangesAsync(context.CancellationToken);
    }

    private async Task<List<OutboxMessage>> GetUnprocessedMessages(CancellationToken cancellationToken) =>
        await dbContext.Set<OutboxMessage>()
            .Where(message => message.ProcessedAt == null && message.Error == null)
            .OrderBy(message => message.CreatedAt)
            .Take(20)
            .ToListAsync(cancellationToken);
}