using Microsoft.AspNetCore.Routing;

namespace Evently.Common.Presentation.EndPoints;

public interface IEndPoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
