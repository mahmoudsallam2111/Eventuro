using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Application.Events.SearchEvents;
using Evently.Modules.Events.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;
public static class SearchEvents
{
    internal static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("searchEvents", async (
            ISender sender,
            Guid? categoryId,
            DateTime? startDate,
            DateTime? endDate,
            int page = 0,
            int pageSize = 15
            ) =>
        {
            Result<SearchEventsResponse> result = await sender.Send(new SearchEventsQuery(categoryId, startDate, endDate , page, pageSize));

            return Results.Ok(result.Value);
        })
        .WithTags(Tags.Events);
    }
}

