namespace CloudStore.Presentation.Requests.Directories;

public sealed record CreateDirectoryRequest(
    string Name,
    Guid? ParentDirectoryId);