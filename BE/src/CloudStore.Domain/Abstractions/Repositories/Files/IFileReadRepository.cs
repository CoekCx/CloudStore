using CloudStore.Domain.Abstractions.Repositories.Base;
using File = CloudStore.Domain.Entities.File;

namespace CloudStore.Domain.Abstractions.Repositories.Files;

public interface IFileReadRepository : IReadRepository<File>
{
    Task<List<File>> GetByIds(List<Guid> ids);
    Task<Guid> GetByDirectoryId(Guid directoryId);
}