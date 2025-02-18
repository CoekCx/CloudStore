using CloudStore.Application.DTOs.Responses.Files;
using MediatR;

namespace CloudStore.Application.Features.Files.GetById;

public record GetFileByIdQuery(Guid DirectoryId, Guid OwnerId) : IRequest<FileResponse>;