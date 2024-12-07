using Evently.Common.Domain;
using Evently.Modules.Events.Application.TicketTypes.UpdateTicketType;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.TicketTypes;
public static class UpdateTicketTypePrice
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("ticket-types/{id}/price", async (Guid id, UpdateTicketTypePriceRequest request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateTicketTypePriceCommand(id, request.Price));

            return result.IsSuccess ? Results.NoContent() : Results.Problem();
        })
            .WithTags(Tags.TicketTypes);
    }

    internal sealed class UpdateTicketTypePriceRequest
    {
        public decimal Price { get; init; }
    }
}
