using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Application.Cart;
public static class CartErrors
{
    public static readonly Error Empty = Error.Problem("Carts.Empty", "The cart is empty");
}

