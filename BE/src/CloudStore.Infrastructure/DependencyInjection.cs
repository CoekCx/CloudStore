using System.Text;
using CloudStore.Application.Abstractions;
using CloudStore.Infrastructure.Authentication;
using CloudStore.Infrastructure.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CloudStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenGenerator, JwtProvider>();
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<IFileFormatter, FileUrlFormatter>();
        services.AddScoped<IFileStorageService, FileStorageService>();

        services.Configure<StorageBucketOptions>(
            configuration.GetSection("StorageBucket"));

        var jwtSection = configuration.GetSection("Jwt");

        services.Configure<JwtOptions>(jwtSection);

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

        return services;
    }
}