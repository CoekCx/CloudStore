namespace CloudStore.Application.Abstractions;

public interface IFileSystemNameGenerator
{
    string GenerateUniqueDirectoryName(string desiredName, Guid? parentDirectoryId);

    Task<string> GenerateUniqueFileName(
        string desiredName,
        Guid parentDirectoryId,
        CancellationToken cancellationToken = default);
}