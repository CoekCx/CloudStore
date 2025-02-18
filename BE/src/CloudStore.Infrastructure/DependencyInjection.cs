using CloudStore.Application.Abstractions;
using CloudStore.Infrastructure.Authentication;
using CloudStore.Infrastructure.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace CloudStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenGenerator, JwtProvider>();
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<IFileFormatter, FileUrlFormatter>();
        services.AddScoped<IFileStorageService, FileStorageService>();

        return services;
    }
}