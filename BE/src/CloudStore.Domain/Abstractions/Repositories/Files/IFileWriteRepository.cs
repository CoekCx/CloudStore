using CloudStore.Domain.Abstractions.Repositories.Base;
using File = CloudStore.Domain.Entities.File;

namespace CloudStore.Domain.Abstractions.Repositories.Files;

public interface IFileWriteRepository : IWriteRepository<File>
{
}