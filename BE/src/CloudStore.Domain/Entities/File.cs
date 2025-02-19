using CloudStore.Domain.Abstractions.Core;
using CloudStore.Domain.EntityIdentifiers;

namespace CloudStore.Domain.Entities;

public class File : Entity<FileId>
{
    public string Url { get; private set; }

    public string Name { get; private set; }

    public string Extension { get; private set; }

    public decimal Size { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }

    public DirectoryId DirectoryId { get; private set; }

    public UserId OwnerId { get; private set; }

    private File(
        FileId id,
        DirectoryId directoryId,
        UserId ownerId,
        string url,
        string name,
        string extension,
        decimal size) : base(id)
    {
        DirectoryId = directoryId;
        OwnerId = ownerId;
        Url = url;
        Name = name;
        Extension = extension;
        Size = size;
        CreatedOnUtc = DateTime.UtcNow;
        ModifiedOnUtc = DateTime.UtcNow;
    }

    public static File Create(
        DirectoryId directoryId,
        UserId ownerId,
        string url,
        string name,
        string extension,
        decimal size)
    {
        return new File(
            new FileId(Guid.NewGuid()),
            directoryId,
            ownerId,
            url,
            name,
            extension,
            size);
    }
}