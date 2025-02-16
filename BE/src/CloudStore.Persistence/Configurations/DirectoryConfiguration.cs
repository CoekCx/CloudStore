using CloudStore.Persistence.Constants;
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

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(255);

        // Self-referencing relationship for parent-child directories
        builder.HasOne(d => d.ParentDirectory)
            .WithMany(d => d.Subdirectories)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        // Relationship with User
        builder.HasOne(d => d.Owner)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}