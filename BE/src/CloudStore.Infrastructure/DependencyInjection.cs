using CloudStore.Application.Abstractions;
using CloudStore.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CloudStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}