namespace CloudStore.Application.Validation;

public static class DirectoryValidatorHelper
{
    public static bool ContainsInvalidCharacters(string name)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        return name.Any(c => invalidChars.Contains(c));
    }
} 