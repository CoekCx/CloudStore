using CloudStore.Application.Responses.Directories;
using MediatR;

namespace CloudStore.Application.UseCases.Directories.Commands.Update;

public sealed record UpdateDirectoryCommand(Guid Id, Guid OwnerId, string NewName) : IRequest<DirectoryResponse>;