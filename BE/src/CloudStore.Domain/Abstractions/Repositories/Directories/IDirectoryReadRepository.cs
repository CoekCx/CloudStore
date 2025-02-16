using CloudStore.Domain.Abstractions.Repositories.Base;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Domain.Abstractions.Repositories.Directories;

public interface IDirectoryReadRepository : IReadRepository<Directory>
{
    Task<List<Directory>> GetChildDirectories(Guid id);
}