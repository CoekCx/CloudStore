using CloudStore.BE.UserAdministration.Persistence.Context;
using CloudStore.BE.UserAdministration.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudStore.BE.UserAdministration.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<UserAdministrationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(UserAdministrationDbContext).Assembly.FullName)));

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}