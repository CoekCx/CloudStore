using CloudStore.Application.Abstractions;
using CloudStore.Domain.Abstractions.Repositories.Directories;

namespace CloudStore.Application.Services;

public class FileSystemNameGenerator(IDirectoryReadRepository directoryReadRepository) : IFileSystemNameGenerator
{
    public string GenerateUniqueDirectoryName(string desiredName, Guid? parentDirectoryId)
    {
        var newName = desiredName;
        var timesRenamed = 0;

        while (directoryReadRepository.DirectoryAlreadyExists(newName, parentDirectoryId))
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
        Guid parentDirectoryId,
        CancellationToken cancellationToken = default)
    {
        var newName = desiredName;
        var timesRenamed = 0;

        var parentDirectory =
            await directoryReadRepository.GetByIdWithContentsAsync(parentDirectoryId, cancellationToken);

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