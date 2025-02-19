using CloudStore.Application.Abstractions;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Repositories.Directories;

namespace CloudStore.Application.Services;

public class FileSystemNameGenerator(IDirectoryReadRepository directoryReadRepository) : IFileSystemNameGenerator
{
    public async Task<string> GenerateUniqueDirectoryName(string desiredName, DirectoryId? parentId)
    {
        var newName = desiredName;
        var timesRenamed = 0;

        while (await directoryReadRepository.ExistsAsync(newName, parentId))
        {
            if (++timesRenamed == 1)
            {
                newName += $" ({timesRenamed})";
                continue;
            }

            newName = newName[..^$"({timesRenamed - 1})".Length] + $"({timesRenamed})";
        }

        return newName;
    }

    public async Task<string> GenerateUniqueFileName(
        string desiredName,
        DirectoryId parentId,
        CancellationToken cancellationToken = default)
    {
        var newName = desiredName;
        var timesRenamed = 0;
        var parentDirectory = await directoryReadRepository.GetByIdWithContentsAsync(parentId, cancellationToken);

        var fileNames = parentDirectory!.Files.Select(f => f.Name).ToList();

        while (fileNames.Contains(newName))
        {
            if (++timesRenamed == 1)
            {
                newName += $" ({timesRenamed})";
                continue;
            }

            newName = newName[..^$"({timesRenamed - 1})".Length] + $"({timesRenamed})";
        }

        return newName;
    }
}