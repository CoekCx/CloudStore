using Microsoft.AspNetCore.Http;

namespace CloudStore.Application.Abstractions;

public interface IFileStorageService
{
    Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default);

    Task<Guid> UploadAsync(byte[] bytes, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid fileId, CancellationToken cancellationToken = default);

    Task<byte[]> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default);
}