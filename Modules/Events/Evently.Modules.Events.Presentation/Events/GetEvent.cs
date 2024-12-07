﻿using Evently.Common.Domain;
using Evently.Modules.Events.Application.Events.GetEvent;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;
public static class GetEvent
{
    internal static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/{id}", async (Guid id, ISender sender) =>
        {
            Result<EventResponse?> @event = await sender.Send(new GetEventQuery(id));

            return @event is null ? Results.NotFound() : Results.Ok(@event.Value);
        })
        .WithTags(Tags.Events);
    }
}
