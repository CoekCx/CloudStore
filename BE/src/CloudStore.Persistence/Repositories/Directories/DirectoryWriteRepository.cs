using CloudStore.Domain.Abstractions.Repositories.Directories;
using CloudStore.Persistence.Context;
using CloudStore.Persistence.Repositories.Base;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Persistence.Repositories.Directories;

public class DirectoryWriteRepository(ApplicationDbContext context)
    : WriteRepository<Directory>(context), IDirectoryWriteRepository
{
} 