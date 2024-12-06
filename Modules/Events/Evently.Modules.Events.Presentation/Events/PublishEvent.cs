using Evently.Modules.Events.Application.Events.PublishEvent;
using Evently.Modules.Events.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;
public static class PublishEvent
{
    internal static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("publishevent/{id}", async (Guid id,ISender sender) =>
        {
         
            Result result = await sender.Send(new PublishEventCommand(id));

            return result.IsSuccess ? Results.Ok(result) : Results.Problem();

        })
        .WithTags(Tags.Events);
    }

}
