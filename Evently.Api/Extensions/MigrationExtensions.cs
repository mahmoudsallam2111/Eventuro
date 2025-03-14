using Evently.Modules.Events.Infrastructure.Database;
using Evently.Modules.Ticketing.Infrastracture.Database;
using Evently.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Api.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder application)
    {
        using IServiceScope scope = application.ApplicationServices.CreateScope();

        // add dbcontext for each module here
        ApplyMigrations<EventsDbContext>(scope);
        ApplyMigrations<UsersDbContext>(scope);
        ApplyMigrations<TicketingDbContext>(scope);
    }

    private static void ApplyMigrations<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();  

        context.Database.Migrate(); 

    }
}
