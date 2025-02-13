using Microsoft.EntityFrameworkCore;
using UserAdministration.Persistence;

namespace Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();

        using var applicationDbContext =
            scope.ServiceProvider.GetRequiredService<UserAdministrationDbContext>();

        applicationDbContext.Database.Migrate();
    }
}