namespace CloudStore.Application.Abstractions;

public interface IDirectoryNameGenerator
{
    string GenerateUniqueName(string desiredName, Guid? parentDirectoryId);
}