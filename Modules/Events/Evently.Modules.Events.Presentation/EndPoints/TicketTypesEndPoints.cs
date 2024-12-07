using Evently.Modules.Events.Domain.TicketTypes;
using Evently.Modules.Events.Presentation.Events;
using Evently.Modules.Events.Presentation.TicketTypes;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.EndPoints;
public static class TicketTypesEndPoints
{
    public static void MapEndPoints(IEndpointRouteBuilder app)
    {
        CreateTicketType.MapEndpoint(app);
        GetTicketType.MapEndpoint(app);
        GetAllTicketTypes.MapEndpoint(app);
        UpdateTicketTypePrice.MapEndpoint(app);
    }
}
