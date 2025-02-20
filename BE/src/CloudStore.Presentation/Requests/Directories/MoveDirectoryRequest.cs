namespace CloudStore.Presentation.Requests.Directories;

public sealed record MoveDirectoryRequest(Guid NewParentDirectoryId);