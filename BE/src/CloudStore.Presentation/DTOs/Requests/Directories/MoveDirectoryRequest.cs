namespace CloudStore.Presentation.DTOs.Requests.Directories;

public sealed record MoveDirectoryRequest(Guid NewParentDirectoryId);