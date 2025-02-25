using CloudStore.Application.UseCases.Directories.Queries.GetById;
using CloudStore.Application.UseCases.Directories.Queries.GetContents;
using CloudStore.Application.UseCases.Directories.Queries.GetRoot;
using CloudStore.Application.UseCases.Users.Queries.GetAll;
using CloudStore.Application.UseCases.Users.Queries.GetById;
using CloudStore.Domain.Repositories;
using CloudStore.Persistence.DataRequests.Directories;
using CloudStore.Persistence.DataRequests.Users;
using CloudStore.Persistence.Interceptors;
using CloudStore.Persistence.Repositories;
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
        
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres"))
                .AddInterceptors(
                    serviceProvider.GetRequiredService<ConvertDomainEventsToMessagesInterceptor>(),
                    serviceProvider.GetRequiredService<UpdateAuditableEntitiesInterceptor>()));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDirectoryRepository, DirectoryRepository>();
        services.AddScoped<IFileRepository, FileRepository>();

        services.AddScoped<IGetAllUsersDataRequest, GetAllUsersDataRequest>();
        services.AddScoped<IGetUserByIdDataRequest, GetUserByIdDataRequest>();
        services.AddScoped<IGetDirectoryByIdDataRequest, GetDirectoryByIdDataRequest>();
        services.AddScoped<IGetDirectoryContentsDataRequest, GetDirectoryContentsDataRequest>();
        services.AddScoped<IGetRootDirectoryDataRequest, GetRootDirectoryDataRequest>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}