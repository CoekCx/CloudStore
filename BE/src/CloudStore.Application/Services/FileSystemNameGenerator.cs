using CloudStore.Application.Abstractions;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Repositories;

namespace CloudStore.Application.Services;

public class FileSystemNameGenerator(IDirectoryRepository directoryRepository) : IFileSystemNameGenerator
{
    public async Task<string> GenerateUniqueDirectoryNameAsync(
        string desiredName,
        DirectoryId? parentId,
        string currentName = "",
        CancellationToken cancellationToken = default)
    {
        var newName = desiredName;
        var timesRenamed = 0;

        while (await directoryRepository.ExistsAsync(newName, parentId, cancellationToken))
        {
            if (!string.IsNullOrEmpty(currentName) && newName == currentName)
            {
                break;
            }
            
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
        string currentName = "",
        CancellationToken cancellationToken = default)
    {
        var newName = desiredName;
        var timesRenamed = 0;
        var parentDirectory = await directoryRepository.GetByIdWithContentsAsync(parentId, cancellationToken);

        var fileNames = parentDirectory!.Files.Select(f => f.Name).ToList();

        while (fileNames.Contains(newName))
        {
            if (!string.IsNullOrEmpty(currentName) && newName == currentName)
            {
                break;
            }
            
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