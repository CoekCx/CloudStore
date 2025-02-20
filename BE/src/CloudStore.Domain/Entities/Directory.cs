using CloudStore.Domain.Abstractions.Core;
using CloudStore.Domain.EntityIdentifiers;

namespace CloudStore.Domain.Entities;

public class Directory : Entity<DirectoryId>
{
    public string Name { get; set; }

    public UserId OwnerId { get; private set; }

    public DirectoryId? ParentDirectoryId { get; set; }

    public ICollection<Directory> Subdirectories { get; private set; }

    public ICollection<File> Files { get; private set; }

    private Directory(DirectoryId id, DirectoryId? parentDirectoryId, string name, UserId ownerId) : base(id)
    {
        Name = name;
        ParentDirectoryId = parentDirectoryId;
        OwnerId = ownerId;
    }

    public static Directory Create(DirectoryId? parentDirectoryId, string name, UserId ownerId) => new(
        new DirectoryId(Guid.NewGuid()),
        parentDirectoryId,
        name,
        ownerId);
}