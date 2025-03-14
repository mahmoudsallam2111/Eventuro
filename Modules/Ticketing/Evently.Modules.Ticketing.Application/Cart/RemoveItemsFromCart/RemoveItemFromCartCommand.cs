using Evently.Common.Application.Messaging;

namespace Evently.Modules.Ticketing.Application.Cart.RemoveItemsFromCart;
public sealed record RemoveItemFromCartCommand(Guid CustomerId, Guid TicketTypeId) : ICommand;

