using Microsoft.AspNetCore.Http;

namespace CloudStore.Presentation.Requests.Files;

public sealed record CreateFileRequest(
    IFormFile File,
    Guid? ParentDirectoryId);