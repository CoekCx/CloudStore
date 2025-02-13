using Microsoft.Extensions.DependencyInjection;
using UserAdministration.Business.Abstractions;
using UserAdministration.Infrastructure.Services;

namespace UserAdministration.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}