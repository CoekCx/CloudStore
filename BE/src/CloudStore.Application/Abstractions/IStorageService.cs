using Microsoft.AspNetCore.Http;

namespace CloudStore.Application.Abstractions;

public interface IStorageService
{
    Task UploadAsync(IFormFile file, string key, CancellationToken cancellationToken = default);

    Task UploadAsync(Stream stream, string key, CancellationToken cancellationToken = default);

    Task DeleteAsync(string key, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default);

    Task<byte[]> DownloadAsync(string key, CancellationToken cancellationToken = default);
}