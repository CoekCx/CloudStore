using CloudStore.Application.Responses.Files;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Application.Responses.Directories;

public sealed record DirectoryContentsResponse(
    Guid DirectoryId,
    string DirectoryName,
    IReadOnlyList<DirectoryResponse> Subdirectories,
    IReadOnlyList<FileResponse> Files)
{
    public static DirectoryContentsResponse FromDirectory(Directory directory)
    {
        var subdirectories = directory.Subdirectories.Select(DirectoryResponse.FromDirectory).ToList();
        var files = directory.Files.Select(FileResponse.FromFile).ToList();
        return new DirectoryContentsResponse(directory.Id.Value, directory.Name, subdirectories, files);
    }
}