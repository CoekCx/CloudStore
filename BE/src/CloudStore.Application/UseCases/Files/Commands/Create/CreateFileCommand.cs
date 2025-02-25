using CloudStore.Application.Responses.Files;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CloudStore.Application.UseCases.Files.Commands.Create;

public record CreateFileCommand(
    IFormFile File,
    Guid? ParentDirectoryId) : IRequest<FileResponse>;