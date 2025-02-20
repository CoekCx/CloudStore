using CloudStore.Domain.Repositories;
using CloudStore.Domain.Repositories.Directories;
using CloudStore.Domain.Repositories.Files;
using CloudStore.Domain.Repositories.Users;
using CloudStore.Persistence.Contexts;
using CloudStore.Persistence.Interceptors;
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
        services.AddSingleton<ConvertDomainEventsToMessagesInterceptor>();
        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        
        services.AddDbContext<WriteDbContext>((serviceProvider, options) =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres"))
                .AddInterceptors(
                    serviceProvider.GetRequiredService<ConvertDomainEventsToMessagesInterceptor>(),
                    serviceProvider.GetRequiredService<UpdateAuditableEntitiesInterceptor>()));

        services.AddDbContext<ReadDbContext>((serviceProvider, options) =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres"))
                .AddInterceptors(
                    serviceProvider.GetRequiredService<ConvertDomainEventsToMessagesInterceptor>(),
                    serviceProvider.GetRequiredService<UpdateAuditableEntitiesInterceptor>()));

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