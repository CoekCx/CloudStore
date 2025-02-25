using CloudStore.Domain.Entities;
using CloudStore.Persistence.Constants;
using CloudStore.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Persistence.Configurations;

public sealed class DirectoryConfiguration : IEntityTypeConfiguration<Directory>
{
    public void Configure(EntityTypeBuilder<Directory> builder)
    {
        builder.ToTable(TableNames.Directories);

        builder.HasKey(d => d.Id);
        
        builder.Property(d => d.Id)
            .HasConversion(EntityIdValueConverters.DirectoryIdConverter);

        builder.Property(d => d.OwnerId)
            .HasConversion(EntityIdValueConverters.UserIdConverter);

        builder.Property(d => d.ParentDirectoryId)
            .HasConversion(EntityIdValueConverters.DirectoryIdConverter!);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(255);

        // Self-referencing relationship for parent-child directories
        builder.HasOne<Directory>()
            .WithMany(d => d.Subdirectories)
            .HasForeignKey(d => d.ParentDirectoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        // Relationship with User
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(d => d.OwnerId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}