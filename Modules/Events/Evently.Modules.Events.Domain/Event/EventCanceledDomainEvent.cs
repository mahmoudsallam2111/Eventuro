using Evently.Modules.Events.Domain.Abstractions;

namespace Evently.Modules.Events.Domain.Event;

public sealed class EventCanceledDomainEvent(Guid eventId) : DomainEvent
{
    public Guid EventId { get; init; } = eventId;
}
