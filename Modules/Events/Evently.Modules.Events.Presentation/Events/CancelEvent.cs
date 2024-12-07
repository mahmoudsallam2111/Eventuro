using Evently.Common.Domain;
using Evently.Modules.Events.Application.Events.CancelEvent;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;
public static class CancelEvent
{
    internal static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("cancel-event/{id}", async (Guid id, ISender sender) =>
        {
            Result result =  await sender.Send(new CancelEventCommand(id));

            return result;
        })
        .WithTags(Tags.Events);
    }
}
