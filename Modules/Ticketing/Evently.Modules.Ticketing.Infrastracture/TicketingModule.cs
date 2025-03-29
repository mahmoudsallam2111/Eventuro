using Evently.Common.Infrastructure.Outbox;
using Evently.Common.Presentation.EndPoints;
using Evently.Modules.Ticketing.Application.Abstractions.Data;
using Evently.Modules.Ticketing.Application.Abstractions.Payment;
using Evently.Modules.Ticketing.Application.Cart;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.Domain.Orders;
using Evently.Modules.Ticketing.Domain.Payments;
using Evently.Modules.Ticketing.Domain.Ticketing;
using Evently.Modules.Ticketing.Infrastracture.Customers;
using Evently.Modules.Ticketing.Infrastracture.Database;
using Evently.Modules.Ticketing.Infrastracture.Events;
using Evently.Modules.Ticketing.Infrastracture.Orders;
using Evently.Modules.Ticketing.Infrastracture.Payments;
using Evently.Modules.Ticketing.Infrastracture.Tickets;
using Evently.Modules.Ticketing.Presentation.Customers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Ticketing.Infrastracture;

public static class TicketingModule
{

    public static IServiceCollection AddTicketingModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddEndPoints(Presentation.AssemblyReference.Assembly);
        services.AddInfrastructue(configuration);

        return services;
    }


    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<UserRegisteredIntegrationEventHandler>();
    }
    private static void AddInfrastructue(this IServiceCollection services , IConfiguration configuration)
    {
        services.AddDbContext<TicketingDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Ticketing))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptors>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TicketingDbContext>());

        services.AddSingleton<CartService>();
        services.AddSingleton<IPaymentService, PaymentService>();
    }
}
