using CloudStore.Domain.Entities;
using CloudStore.Persistence.Constants;
using CloudStore.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Directory = CloudStore.Domain.Entities.Directory;
using File = CloudStore.Domain.Entities.File;

namespace CloudStore.Persistence.Configurations;

public sealed class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable(TableNames.Files);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasConversion(EntityIdValueConverters.FileIdConverter);

        builder.Property(f => f.DirectoryId)
            .HasConversion(EntityIdValueConverters.DirectoryIdConverter);

        builder.Property(f => f.OwnerId)
            .HasConversion(EntityIdValueConverters.UserIdConverter);

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
        builder.HasOne<Directory>()
            .WithMany(d => d.Files)
            .HasForeignKey(f => f.DirectoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // Relationship with User
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(f => f.OwnerId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}