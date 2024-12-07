using Evently.Common.Domain;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Application.TicketTypes.GetTicketType;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using GetTicketTypeResponse = Evently.Modules.Events.Application.TicketTypes.GetTicketType.GetTicketTypeResponse;

namespace Evently.Modules.Events.Presentation.TicketTypes;
public static class GetTicketType
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("ticket-types/{id}", async (Guid id, ISender sender) =>
        {
            Result<GetTicketTypeResponse> result = await sender.Send(new GetTicketTypeQuery(id));

            return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound();
        })
        .WithTags(Tags.TicketTypes);
    }
}
