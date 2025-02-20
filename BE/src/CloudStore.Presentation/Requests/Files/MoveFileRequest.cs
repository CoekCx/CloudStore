namespace CloudStore.Presentation.Requests.Files;

public sealed record MoveFileRequest(Guid NewParentDirectoryId);