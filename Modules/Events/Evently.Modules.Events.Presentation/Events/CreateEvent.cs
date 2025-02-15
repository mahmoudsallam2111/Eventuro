using Evently.Common.Domain;
using Evently.Common.Presentation.EndPoints;
using Evently.Modules.Events.Application.Events.CreateEvent;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;


namespace Evently.Modules.Events.Presentation.Events;


public class CreateEvent : IEndPoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (CreateEventRequest request, ISender sender) =>
        {


            var command =new CreateEventCommand(request.CategoryId, request.Title, request.Description, request.Location,
                request.StartsAtUtc, request.EndsAtUtc);

            Result<Guid> eventId = await sender.Send(command);

            return Results.Ok(eventId.Value);
        })
        .WithTags(Tags.Events);
    }

    internal sealed class CreateEventRequest
    {
        public Guid CategoryId { get; init; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime StartsAtUtc { get; set; }

        public DateTime? EndsAtUtc { get; set; }
    }
}
