using System.Text;
using CloudStore.Api.ServiceInstallers.OptionsSetups;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CloudStore.Api.ServiceInstallers;

public static class SecurityServiceInstaller
{
    public static void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        InstallOptions(services);

        InstallCore(services, configuration);
    }

    private static void InstallOptions(IServiceCollection services)
    {
        services.ConfigureOptions<JwtOptionsSetup>();
    }

    private static void InstallCore(IServiceCollection services, IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection("Jwt");
        var secretKey = jwtSection["SecretKey"];
        var issuer = jwtSection["Issuer"];
        var audience = jwtSection["Audience"];

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretKey!))
                };
            });
    }
}