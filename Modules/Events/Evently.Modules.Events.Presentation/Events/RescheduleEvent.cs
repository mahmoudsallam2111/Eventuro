using Evently.Modules.Events.Application.Events.RescheduledEvent;
using Evently.Modules.Events.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;
public static class RescheduleEvent
{
    internal static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("reschedule/{id}", async (Guid id , RescheduleEventRequest request ,   ISender sender) =>
        {
           
            Result result = await sender.Send(new RescheduleEventCommand(id, request.StartsAtUtc , request.EndsAtUtc));

            return result.IsSuccess ? Results.Ok(result) : Results.Problem();

        })
        .WithTags(Tags.Events);
    }
}


internal sealed class RescheduleEventRequest
{
    public DateTime StartsAtUtc { get; init; }

    public DateTime? EndsAtUtc { get; init; }
}
