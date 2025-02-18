using CloudStore.Infrastructure.Storage;
using Microsoft.Extensions.Options;

namespace CloudStore.Api.ServiceInstallers.OptionsSetups;

public class StorageBucketOptionsSetup(IConfiguration configuration) : IConfigureOptions<StorageBucketOptions>
{
    private const string SectionName = "StorageBucket";

    public void Configure(StorageBucketOptions options)
    {
        configuration
            .GetSection(SectionName)
            .Bind(options);
    }
}