using System.Text;
using CloudStore.Application;
using CloudStore.Infrastructure;
using CloudStore.Infrastructure.Authentication;
using CloudStore.Infrastructure.Storage;
using CloudStore.Persistence;
using CloudStore.Presentation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Dependency Injection
services.AddPersistence(configuration);
services.AddInfrastructure();
services.AddApplication();
services.AddPresentation();

// Options configuration
services.Configure<StorageBucketOptions>(
    builder.Configuration.GetSection("StorageBucket"));

var jwtSection = configuration.GetSection("Jwt");
services.Configure<JwtOptions>(jwtSection);

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();