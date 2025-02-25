using MediatR;

namespace CloudStore.Application.UseCases.Files.Commands.Delete;

public sealed record DeleteFileCommand(Guid FileId, Guid OwnerId) : IRequest<Unit>;