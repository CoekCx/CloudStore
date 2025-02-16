using CloudStore.Domain.Abstractions.Repositories.Base;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Domain.Abstractions.Repositories.Directories;

public interface IDirectoryWriteRepository : IWriteRepository<Directory>
{
}