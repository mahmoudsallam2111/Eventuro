using Evently.Common.Application.Caching;
using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Common.Presentation.EndPoints;
using Evently.Modules.Events.Application.Categories.GetCategories;
using Evently.Modules.Events.Application.Categories.GetCategory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Categories;
public class GetCategories : IEndPoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("categories", async (ISender sender , ICachingService cachingService) =>
        {

            IReadOnlyCollection<CategoryResponse> categoryResponse = 
                                          await cachingService.GetAsync<IReadOnlyCollection<CategoryResponse>>(nameof(GetCategories));

            if (categoryResponse is not null)
            {
                return Results.Ok(categoryResponse);
            }

            // if caching is null ,  the i will query the database ,  then set the value 
            Result<IReadOnlyCollection<CategoryResponse>> result = await sender.Send(new GetCategoriesQuery());

            if (result.IsSuccess)
            {
               await cachingService.SetAsync(nameof(GetCategories), result.Value);
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Categories);
    }
}
