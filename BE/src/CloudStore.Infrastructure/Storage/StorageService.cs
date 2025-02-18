using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using CloudStore.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CloudStore.Infrastructure.Storage;

public class StorageService(IAmazonS3 amazonS3Client, IOptions<StorageBucketOptions> storageBucketOptions)
    : IStorageService
{
    private const string KeyDoesNotExistErrorMessage = "The specified key does not exist.";
    private readonly StorageBucketOptions _storageBucketOptions = storageBucketOptions.Value;

    public async Task UploadAsync(IFormFile file, string key, CancellationToken cancellationToken = default)
    {
        var transferUtilityUploadRequest = new TransferUtilityUploadRequest
        {
            BucketName = _storageBucketOptions.BucketName,
            Key = key,
            InputStream = file.OpenReadStream(),
            AutoCloseStream = true
        };

        using var transferUtility = new TransferUtility(amazonS3Client);

        await transferUtility.UploadAsync(transferUtilityUploadRequest, cancellationToken);
    }

    public async Task UploadAsync(Stream stream, string key, CancellationToken cancellationToken = default)
    {
        var transferUtilityUploadRequest = new TransferUtilityUploadRequest
        {
            BucketName = _storageBucketOptions.BucketName,
            Key = key,
            InputStream = stream,
            AutoCloseStream = true
        };

        using var transferUtility = new TransferUtility(amazonS3Client);

        await transferUtility.UploadAsync(transferUtilityUploadRequest, cancellationToken);
    }

    public async Task DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = _storageBucketOptions.BucketName,
            Key = key
        };

        await amazonS3Client.DeleteObjectAsync(deleteObjectRequest, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            var getObjectResponse = await amazonS3Client
                .GetObjectAsync(_storageBucketOptions.BucketName, key, cancellationToken);

            return getObjectResponse?.HttpStatusCode is HttpStatusCode.OK;
        }
        catch (AmazonS3Exception amazonS3Exception) when (amazonS3Exception.Message == KeyDoesNotExistErrorMessage)
        {
            return false;
        }
    }

    public async Task<byte[]> DownloadAsync(string key, CancellationToken cancellationToken = default)
    {
        var memoryStream = new MemoryStream();
        using (var getObjectResponse =
               await amazonS3Client.GetObjectAsync(_storageBucketOptions.BucketName, key, cancellationToken))
        {
            await getObjectResponse.ResponseStream.CopyToAsync(memoryStream, cancellationToken);
        }

        return memoryStream.ToArray();
    }
}