using Evently.Common.Infrastructure.Outbox;
using Evently.Common.Presentation.EndPoints;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.EventStatistics;
using Evently.Modules.Events.Domain.Category;
using Evently.Modules.Events.Domain.Event;
using Evently.Modules.Events.Domain.TicketTypes;
using Evently.Modules.Events.Infrastructure.Categories;
using Evently.Modules.Events.Infrastructure.Database;
using Evently.Modules.Events.Infrastructure.Event;
using Evently.Modules.Events.Infrastructure.EventStatistics;
using Evently.Modules.Events.Infrastructure.Outbox;
using Evently.Modules.Events.Infrastructure.PublicApi;
using Evently.Modules.Events.Infrastructure.TicketTypes;
using Evently.Modules.Events.PublicApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Events.Infrastructure;

public static class EventsModule
{

    public static IServiceCollection AddEventsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddEndPoints(Presentation.AssemblyReference.Assembly);
        services.AddInfrastructue(configuration);

        return services;
    }

    private static void AddInfrastructue(this IServiceCollection services , IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<EventsDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptors>()));


        // Register Services
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IEventStatisticsRepository, EventStatisticsRepository>();

        services.AddScoped<IEventApi, EventApi>();


        // this is option pattern
        services.AddOptions<DocumentDbSettings>()
            .BindConfiguration("MongoSettings")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.Configure<OutboxOptions>(configuration.GetSection("Events:Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob>();
    }
}
