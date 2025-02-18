using CloudStore.Application.Responses.Directories;
using MediatR;

namespace CloudStore.Application.UseCases.Directories.GetRoot;

public sealed record GetRootDirectoryQuery(Guid OwnerId) : IRequest<DirectoryResponse>;