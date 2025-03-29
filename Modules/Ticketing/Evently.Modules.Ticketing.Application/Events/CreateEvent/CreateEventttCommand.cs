using Evently.Common.Application.Messaging;

namespace Evently.Modules.Ticketing.Application.Events.CreateEvent;
public sealed record CreateEventttCommand(
    Guid EventId,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc,
    List<CreateEventttCommand.TicketTypeRequest> TicketTypes) : ICommand
{
    public sealed record TicketTypeRequest(
        Guid TicketTypeId,
        Guid EventId,
        string Name,
        decimal Price,
        string Currency,
        decimal Quantity);
}
