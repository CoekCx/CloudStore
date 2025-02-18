namespace CloudStore.Presentation.DTOs.Requests.Files;

public sealed record MoveFileRequest(Guid NewParentDirectoryId);