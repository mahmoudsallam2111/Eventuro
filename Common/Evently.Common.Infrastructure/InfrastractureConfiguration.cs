using Evently.Common.Application.Caching;
using Evently.Common.Application.Clock;
using Evently.Common.Application.Data;
using Evently.Common.Application.EventBus;
using Evently.Common.Infrastructure.Authentication;
using Evently.Common.Infrastructure.Caching;
using Evently.Common.Infrastructure.Clock;
using Evently.Common.Infrastructure.Data;
using Evently.Common.Infrastructure.EventBus;
using Evently.Common.Infrastructure.Interceptors;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using StackExchange.Redis;

namespace Evently.Common.Infrastructure;

public static class InfrastractureConfiguration
{
    public static IServiceCollection AddInfrastracture(
        this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigurationConsumers,
        string databaseConnectionString ,
        string redisConnectionString)
    {

        services.AddAuthenticationInternal();

        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();


        services.TryAddSingleton<PublishDomainEventsInterceptors>();   // regiter PublishDomainEventsInterceptors


        #region register caching dependencies

        // this is for redis
        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.TryAddSingleton(connectionMultiplexer);

            services.AddStackExchangeRedisCache(options =>
            {
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer);
            });
        }
        catch
        {

            services.AddDistributedMemoryCache();   // to solve migration issue and prvent app from stopping by adding fallback behv.
        }


        services.TryAddSingleton<ICachingService , CachingService>();
        #endregion

        services.TryAddSingleton<IEventBus, EventBus.EventBus>();

        // register mass transint
        services.AddMassTransit(configs =>
        {
            foreach (Action<IRegistrationConfigurator> configurationConsumer in moduleConfigurationConsumers)
            {
                configurationConsumer(configs);
            }

            configs.SetKebabCaseEndpointNameFormatter();

            configs.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
