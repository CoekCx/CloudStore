using CloudStore.Persistence.Constants;
using CloudStore.Persistence.Outbox;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CloudStore.Persistence.Configurations;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(TableNames.OutboxMessages);

        builder.HasKey(message => message.Id);

        builder.Property(message => message.Content).IsRequired().HasColumnType("json");

        builder.Property(message => message.CreatedAt).IsRequired();

        builder.Property(message => message.Type).IsRequired();

        builder.Property(message => message.ProcessedAt).IsRequired(false);

        builder.Property(message => message.Error).IsRequired(false);
    }
}