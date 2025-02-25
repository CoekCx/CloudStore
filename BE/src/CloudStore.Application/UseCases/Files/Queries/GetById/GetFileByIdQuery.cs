using CloudStore.Application.Responses.Files;
using MediatR;

namespace CloudStore.Application.UseCases.Files.Queries.GetById;

public record GetFileByIdQuery(Guid DirectoryId, Guid OwnerId) : IRequest<FileResponse>;