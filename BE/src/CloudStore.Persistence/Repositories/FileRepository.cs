using CloudStore.Domain.Repositories;

namespace CloudStore.Persistence.Repositories;

public class FileRepository(ApplicationDbContext context) : IFileRepository;