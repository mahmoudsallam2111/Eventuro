using Evently.Common.Domain;
using Evently.Common.Presentation.EndPoints;
using Evently.Modules.Events.Application.EventStatistics.GetEventStatisics;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.EventStatistics;

public class GetEventStatistics : IEndPoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("event-statistics/{id}", async (Guid id, ISender sender) =>
            {
                Result<Application.EventStatistics.EventStatistics> result = await sender.Send(new GetEventStatisticsQuery(id));

                return result.IsSuccess ? Results.Ok(result.Value) : Results.Problem();
            })
            .WithTags(Tags.EventStatistics);
    }
}
