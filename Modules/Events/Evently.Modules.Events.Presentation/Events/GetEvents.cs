using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Application.Events.GetEvents;
using Evently.Modules.Events.Domain.Abstractions;
using Evently.Modules.Events.Domain.Event;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Evently.Modules.Events.Presentation.Events;
public static class GetEvents
{
    internal static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<GetEventResponse>> result = await sender.Send(new GetEventsQuery());
            return result is null ? Results.NotFound() : Results.Ok(result.Value);

        })
        .WithTags(Tags.Events);
    }
}
