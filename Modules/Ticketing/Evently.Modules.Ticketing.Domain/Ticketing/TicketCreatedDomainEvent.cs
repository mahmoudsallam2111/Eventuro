using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Ticketing;
public sealed class TicketCreatedDomainEvent(Guid ticketId) : DomainEvent
{
    public Guid TicketId { get; init; } = ticketId;
}
