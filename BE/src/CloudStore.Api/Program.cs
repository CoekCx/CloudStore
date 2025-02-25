using CloudStore.Application;
using CloudStore.Infrastructure;
using CloudStore.Persistence;
using CloudStore.Presentation;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(configuration)
    .AddPersistence(configuration);

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