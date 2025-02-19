using CloudStore.Domain.EntityIdentifiers;

namespace CloudStore.Application.Abstractions;

public interface IFileSystemNameGenerator
{
    Task<string> GenerateUniqueDirectoryName(string desiredName, DirectoryId? parentId);

    Task<string> GenerateUniqueFileName(
        string desiredName,
        DirectoryId parentId,
        CancellationToken cancellationToken = default);
}