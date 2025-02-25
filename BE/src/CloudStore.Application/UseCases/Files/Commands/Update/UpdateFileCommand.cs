using CloudStore.Application.Responses.Files;
using MediatR;

namespace CloudStore.Application.UseCases.Files.Commands.Update;

public record UpdateFileCommand(Guid Id, Guid OwnerId, string NewName) : IRequest<FileResponse>;