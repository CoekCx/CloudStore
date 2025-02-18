using MediatR;

namespace CloudStore.Application.Features.Files.Delete;

public sealed record DeleteFileCommand(Guid FileId, Guid OwnerId) : IRequest<Unit>;