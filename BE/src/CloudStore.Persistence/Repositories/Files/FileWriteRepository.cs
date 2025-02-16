using CloudStore.Domain.Abstractions.Repositories.Files;
using CloudStore.Persistence.Context;
using CloudStore.Persistence.Repositories.Base;
using File = CloudStore.Domain.Entities.File;

namespace CloudStore.Persistence.Repositories.Files;

public class FileWriteRepository(ApplicationDbContext context) 
    : WriteRepository<File>(context), IFileWriteRepository
{
} 