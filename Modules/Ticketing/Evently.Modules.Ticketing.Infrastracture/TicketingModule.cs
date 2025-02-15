using Evently.Common.Application.Caching;
using Evently.Common.Infrastructure.Interceptors;
using Evently.Common.Presentation.EndPoints;
using Evently.Modules.Ticketing.Application.Cart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Events.Infrastructure;

public static class TicketingModule
{

    public static IServiceCollection AddTicketingModule(
        this IServiceCollection services)
    {

        services.AddEndPoints(Ticketing.Presentation.AssemblyReference.Assembly);
        services.AddInfrastructue();

        return services;
    }

    private static void AddInfrastructue(this IServiceCollection services)
    {
        services.AddSingleton<CartService>();
    }
}
