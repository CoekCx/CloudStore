namespace CloudStore.Domain.Entities;

public class Directory : BaseEntity
{
    protected Directory()
    {
        Subdirectories = [];
        Files = [];
    }

    public Directory(Directory? parentDirectory, string name, User owner) : this()
    {
        Id = Guid.NewGuid();
        Name = name;
        ParentDirectory = parentDirectory;
        Owner = owner;
    }

    public string Name { get; set; }

    // Navigation properties
    public Directory? ParentDirectory { get; private set; }
    public ICollection<Directory> Subdirectories { get; private set; }
    public ICollection<File> Files { get; private set; }
    public User Owner { get; private set; }
}