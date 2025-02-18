using CloudStore.Application.DTOs.Responses.Directories;
using MediatR;

namespace CloudStore.Application.Features.Directories.GetRoot;

public sealed record GetRootDirectoryQuery(Guid OwnerId) : IRequest<DirectoryResponse>;