using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Evently.Common.Presentation.EndPoints;
public static class EndPointsExtensions
{
    public static IServiceCollection AddEndPoints(this IServiceCollection services,params Assembly[] assemblies)
    {
        ServiceDescriptor[] servicesDescriptors = assemblies
                                           .SelectMany(a => a.GetTypes())
                                           .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                                               type.IsAssignableTo(typeof(IEndPoint)))
                                               .Select(type => ServiceDescriptor.Transient(typeof(IEndPoint), type))
                                               .ToArray();


        services.TryAddEnumerable(servicesDescriptors);
        return services;
    }


    public static IApplicationBuilder MapEndPoints(this WebApplication app , 
        RouteGroupBuilder? routeGroupBuilder  = null)
    {
        IEnumerable<IEndPoint> endPoints = app.Services.GetRequiredService<IEnumerable<IEndPoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndPoint endPoint in endPoints)
        {
            endPoint.MapEndpoint(builder);
        }
        return app;
    }
}
