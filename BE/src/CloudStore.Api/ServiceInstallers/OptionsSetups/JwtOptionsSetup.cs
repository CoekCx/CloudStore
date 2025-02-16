using CloudStore.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace CloudStore.Api.ServiceInstallers.OptionsSetups;

public class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "Jwt";

    public void Configure(JwtOptions options)
    {
        configuration
            .GetSection(SectionName)
            .Bind(options);
    }
}