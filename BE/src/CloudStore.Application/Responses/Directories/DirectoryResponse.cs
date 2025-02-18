using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Application.Responses.Directories;

public sealed record DirectoryResponse(
    Guid Id,
    string Name,
    Guid? ParentDirectoryId,
    Guid OwnerId)
{
    public static DirectoryResponse FromDirectory(Directory directory)
    {
        return new DirectoryResponse(
            directory.Id,
            directory.Name,
            directory.ParentDirectoryId,
            directory.OwnerId);
    }
}