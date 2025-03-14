using Evently.Common.Application.Messaging;

namespace Evently.Modules.Ticketing.Application.Cart.GetCart;
public sealed record GetCartQuery(Guid CustomerId) : IQuery<Cart>;
