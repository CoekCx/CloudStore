using MediatR;

namespace CloudStore.Application.Features.Files.Move;

public record MoveFileCommand(
    Guid FileId,
    Guid OwnerId,
    Guid? NewParentDirectoryId) : IRequest<Unit>;