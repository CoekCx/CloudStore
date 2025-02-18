namespace CloudStore.Presentation.DTOs.Requests.Directories;

public sealed record CreateDirectoryRequest(
    string Name,
    Guid? ParentDirectoryId);