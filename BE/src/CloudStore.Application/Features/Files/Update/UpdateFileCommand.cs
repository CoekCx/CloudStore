using CloudStore.Application.DTOs.Responses.Files;
using MediatR;

namespace CloudStore.Application.Features.Files.Update;

public record UpdateFileCommand(Guid Id, Guid OwnerId, string NewName) : IRequest<FileResponse>;