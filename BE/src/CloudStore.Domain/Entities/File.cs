namespace CloudStore.Domain.Entities;

public class File : BaseEntity
{
    protected File()
    {
    }

    public File(Directory parentDirectory, User owner, string url, string name, string extension, decimal size)
        : this()
    {
        Id = Guid.NewGuid();
        ParentDirectory = parentDirectory;
        Owner = owner;
        Url = url;
        Name = name;
        Extension = extension;
        Size = size;
        CreatedOnUtc = DateTime.UtcNow;
        ModifiedOnUtc = DateTime.UtcNow;
    }

    public string Url { get; private set; }

    public string Name { get; private set; }

    public string Extension { get; private set; }

    public decimal Size { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ModifiedOnUtc { get; private set; }

    // Navigation properties
    public Directory ParentDirectory { get; private set; }
    public User Owner { get; private set; }
}