using Evently.Common.Application.Messaging;

namespace Evently.Modules.Ticketing.Application.Cart.ClearCart;
public sealed record ClearCartCommand(Guid CustomerId) : ICommand;
