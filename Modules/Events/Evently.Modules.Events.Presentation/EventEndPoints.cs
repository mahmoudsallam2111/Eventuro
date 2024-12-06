using Evently.Modules.Events.Application.Events;
using Evently.Modules.Events.Presentation.Events;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation;
public static class EventEndPoints
{
    public static void MapEndPoints(IEndpointRouteBuilder app)
    {
        CreateEvent.MapEndpoint(app);
        GetEvent.MapEndpoint(app);
        GetEvents.MapEndpoint(app);
        CancelEvent.MapEndpoint(app);
        PublishEvent.MapEndpoint(app);
        RescheduleEvent.MapEndpoint(app);
        SearchEvents.MapEndpoint(app);
    }
}
