using CloudStore.Application.Abstractions;
using CloudStore.Domain.Abstractions.Repositories.Directories;

namespace CloudStore.Application.Services;

public class DirectoryNameGenerator(IDirectoryReadRepository directoryReadRepository) : IDirectoryNameGenerator
{
    public string GenerateUniqueName(string desiredName, Guid? parentDirectoryId)
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
}