namespace CloudStore.Domain.Entities;

public class Directory : BaseEntity
{
    protected Directory()
    {
        Id = Guid.NewGuid();
        Subdirectories = [];
        Files = [];
    }

    public Directory(Guid? parentDirectoryId, string name, Guid ownerId) : this()
    {
        Name = name;
        ParentDirectoryId = parentDirectoryId;
        OwnerId = ownerId;
    }

    public string Name { get; set; }

    // Foreign keys
    public Guid OwnerId { get; set; }
    public Guid? ParentDirectoryId { get; set; }

    // Navigation properties
    public ICollection<Directory> Subdirectories { get; private set; }
    public ICollection<File> Files { get; private set; }
}