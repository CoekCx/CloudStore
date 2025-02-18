using MediatR;

namespace CloudStore.Application.UseCases.Files.Delete;

public sealed record DeleteFileCommand(Guid FileId, Guid OwnerId) : IRequest<Unit>;