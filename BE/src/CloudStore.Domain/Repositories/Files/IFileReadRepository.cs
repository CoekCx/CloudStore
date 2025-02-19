using CloudStore.Domain.EntityIdentifiers;
using File = CloudStore.Domain.Entities.File;

namespace CloudStore.Domain.Repositories.Files;

public interface IFileReadRepository
{
    Task<List<File>> GetByIds(List<FileId> ids, CancellationToken cancellationToken);

    Task<File?> GetById(FileId id, CancellationToken cancellationToken);
}