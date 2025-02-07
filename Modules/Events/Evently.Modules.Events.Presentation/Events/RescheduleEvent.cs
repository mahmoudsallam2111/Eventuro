﻿using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Common.Presentation.EndPoints;
using Evently.Modules.Events.Application.Events.RescheduledEvent;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;
public class RescheduleEvent : IEndPoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("reschedule/{id}", async (Guid id , RescheduleEventRequest request ,   ISender sender) =>
        {
           
            Result result = await sender.Send(new RescheduleEventCommand(id, request.StartsAtUtc , request.EndsAtUtc));

            return result.Match(Results.NoContent, ApiResults.Problem);

        })
        .WithTags(Tags.Events);
    }
}


internal sealed class RescheduleEventRequest
{
    public DateTime StartsAtUtc { get; init; }

    public DateTime? EndsAtUtc { get; init; }
}
