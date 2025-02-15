using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Common.Presentation.EndPoints;
using Evently.Modules.Ticketing.Application.Cart.AddItemsToCart;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Ticketing.Presentation.Cart;
public sealed class AddToCart : IEndPoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("cart/add", async (AddToCartRequest request, ISender sender) =>
        {
            Result result = await sender.Send(new AddItemToCartCommand(request.CustomerId,
                request.TicketTypeId,
                request.Quantity));

            return result.Match(()=> Results.Ok() , ApiResults.Problem);
        }).WithTags(Tags.Carts);
    }
}

internal sealed class AddToCartRequest
{
    public Guid CustomerId { get;init; }
    public Guid TicketTypeId { get;init; }
    public decimal Quantity { get;init; }
}
