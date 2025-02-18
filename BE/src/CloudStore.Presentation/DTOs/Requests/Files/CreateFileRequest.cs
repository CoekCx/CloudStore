using Microsoft.AspNetCore.Http;

namespace CloudStore.Presentation.DTOs.Requests.Files;

public sealed record CreateFileRequest(
    IFormFile File,
    Guid? ParentDirectoryId);