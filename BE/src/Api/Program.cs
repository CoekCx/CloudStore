using Api.Extensions;
using Carter;
using UserAdministration.Business;
using UserAdministration.Infrastructure;
using UserAdministration.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddBusiness();
builder.Services.AddInfrastructure();
builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();

app.UseHttpsRedirection();

app.MapCarter();

app.Run();