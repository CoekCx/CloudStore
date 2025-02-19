using CloudStore.Domain.Repositories.Files;
using CloudStore.Persistence.Contexts;

namespace CloudStore.Persistence.Repositories.Files;

public class FileWriteRepository(WriteDbContext context) : IFileWriteRepository;