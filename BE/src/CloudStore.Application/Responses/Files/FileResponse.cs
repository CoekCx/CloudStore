using File = CloudStore.Domain.Entities.File;

namespace CloudStore.Application.Responses.Files;

public sealed record FileResponse(
    Guid Id,
    string Name,
    string Extension,
    string Url,
    decimal Size,
    Guid ParentDirectoryId,
    Guid OwnerId,
    DateTime CreatedOnUtc,
    DateTime? ModifiedOnUtc)
{
    public static FileResponse FromFile(File file)
    {
        return new FileResponse(
            file.Id,
            file.Name,
            file.Extension,
            file.Url,
            file.Size,
            file.ParentDirectoryId,
            file.OwnerId,
            file.CreatedOnUtc,
            file.ModifiedOnUtc);
    }
}