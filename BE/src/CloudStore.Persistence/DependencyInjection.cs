using CloudStore.Domain.Abstractions;
using CloudStore.Domain.Abstractions.Repositories.Base;
using CloudStore.Domain.Abstractions.Repositories.Directories;
using CloudStore.Domain.Abstractions.Repositories.Files;
using CloudStore.Domain.Abstractions.Repositories.Users;
using CloudStore.Persistence.Context;
using CloudStore.Persistence.Repositories.Base;
using CloudStore.Persistence.Repositories.Directories;
using CloudStore.Persistence.Repositories.Files;
using CloudStore.Persistence.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudStore.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContexts
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddDbContext<ReadOnlyApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        // Register Generic Repositories
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

        // Register Specific Repositories
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();
        services.AddScoped<IDirectoryReadRepository, DirectoryReadRepository>();
        services.AddScoped<IDirectoryWriteRepository, DirectoryWriteRepository>();
        services.AddScoped<IFileReadRepository, FileReadRepository>();
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}