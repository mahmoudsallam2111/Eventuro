using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Common.Presentation.EndPoints;
using Evently.Modules.Events.Application.TicketTypes.GetAllTicketTypes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using GetTicketTypeResponse = Evently.Modules.Events.Application.TicketTypes.GetTicketType.GetTicketTypeResponse;

namespace Evently.Modules.Events.Presentation.TicketTypes;
public class GetAllTicketTypes : IEndPoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("ticket-types", async (ISender sender) =>
        {
            Result<IReadOnlyCollection<GetTicketTypeResponse>> result = await sender.Send(
                new GetAllTicketTypesQuery());

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.TicketTypes);
    }
}
