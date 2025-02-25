using CloudStore.Application.Responses.Directories;
using CloudStore.Domain.Abstractions.Core;
using CloudStore.Domain.EntityIdentifiers;

namespace CloudStore.Application.UseCases.Directories.Queries.GetContents;

public interface IGetDirectoryContentsDataRequest : IDataRequest<DirectoryId, DirectoryContentsResponse>;