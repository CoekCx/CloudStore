using CloudStore.Domain.EntityIdentifiers;

namespace CloudStore.Application.Abstractions;

public interface IFileSystemNameGenerator
{
    Task<string> GenerateUniqueDirectoryNameAsync(
        string desiredName,
        DirectoryId? parentId,
        string currentName = "",
        CancellationToken cancellationToken = default);

    Task<string> GenerateUniqueFileName(
        string desiredName,
        DirectoryId parentId,
        string currentName = "",
        CancellationToken cancellationToken = default);
}