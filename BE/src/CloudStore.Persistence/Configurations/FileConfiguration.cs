using CloudStore.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = CloudStore.Domain.Entities.File;

namespace CloudStore.Persistence.Configurations;

public sealed class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable(TableNames.Files);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(f => f.Extension)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(f => f.Url)
            .IsRequired();

        builder.Property(f => f.Size)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(f => f.CreatedOnUtc)
            .IsRequired();

        builder.Property(f => f.ModifiedOnUtc);

        // Relationship with Directory
        builder.HasOne(f => f.ParentDirectory)
            .WithMany(d => d.Files)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // Relationship with User
        builder.HasOne(f => f.Owner)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}