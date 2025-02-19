using System.Text.Json;
using CloudStore.Persistence.Contexts;
using CloudStore.Persistence.Outbox;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace CloudStore.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class ProcessOutboxJob(ReadDbContext dbContext, IPublishEndpoint publishEndpoint) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await GetUnprocessedMessages(context.CancellationToken);

        if (!messages.Any())
        {
            return;
        }

        foreach (OutboxMessage message in messages)
        {
            try
            {
                var type = typeof(Domain.AssemblyReference).Assembly.GetType(message.Type)!;
                var deserializedEvent = JsonSerializer.Deserialize(message.Content, type)!;

                await publishEndpoint.Publish(deserializedEvent, type, context.CancellationToken);

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