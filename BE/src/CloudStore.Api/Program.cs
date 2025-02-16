using CloudStore.Api.ServiceInstallers;
using CloudStore.Application;
using CloudStore.Infrastructure;
using CloudStore.Persistence;
using CloudStore.Presentation;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Dependency Injection
services.AddPersistence(configuration);
services.AddInfrastructure();
services.AddApplication();
services.AddPresentation();

SecurityServiceInstaller.InstallServices(services, configuration);

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