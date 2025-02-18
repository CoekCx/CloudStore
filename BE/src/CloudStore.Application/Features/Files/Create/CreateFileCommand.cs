using CloudStore.Application.DTOs.Responses.Files;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CloudStore.Application.Features.Files.Create;

public record CreateFileCommand(
    IFormFile File,
    Guid? ParentDirectoryId) : IRequest<FileResponse>;