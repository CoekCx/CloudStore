using System.Text;
using Amazon.S3;
using CloudStore.Application.Abstractions;
using CloudStore.Infrastructure.Authentication;
using CloudStore.Infrastructure.Jobs;
using CloudStore.Infrastructure.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;

namespace CloudStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection("Jwt");

        services.Configure<JwtOptions>(jwtSection);

        services.Configure<StorageBucketOptions>(
            configuration.GetSection("StorageBucket"));
        
        services.AddAWSService<IAmazonS3>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenGenerator, JwtProvider>();
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<IFileFormatter, FileUrlFormatter>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidAudience = jwtSection["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSection["SecretKey"]!))
                };
            });
        
        services.AddQuartz();

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        
        services.AddQuartz(q =>
        {
            q.AddJob<ProcessOutboxJob>(c => c
                .StoreDurably()
                .WithIdentity(nameof(ProcessOutboxJob)));

            q.AddTrigger(opts => opts
                .ForJob(nameof(ProcessOutboxJob))
                .WithIdentity(nameof(ProcessOutboxJob))
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever()));
        });

        return services;
    }
}