using System.IO;
using System.Linq;

namespace CloudStore.Application.Helpers;

public static class DirectoryValidatorHelper
{
    public static bool ContainsInvalidCharacters(string name)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        return name.Any(c => invalidChars.Contains(c));
    }
} 