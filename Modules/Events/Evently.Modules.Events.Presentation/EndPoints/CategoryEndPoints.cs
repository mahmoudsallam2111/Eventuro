using Evently.Modules.Events.Presentation.Categories;
using Evently.Modules.Events.Presentation.Category;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.EndPoints;
public static class CategoryEndPoints
{
    public static void MapEndPoints(IEndpointRouteBuilder app)
    {
        CreateCategory.MapEndpoint(app);
        ArchiveCategory.MapEndpoint(app);
        GetCategories.MapEndpoint(app);
        GetCategory.MapEndpoint(app);
        UpdateCategory.MapEndpoint(app);
    }
}
