using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Event;

public sealed class EventCreatedDomainEvent(Guid eventid) : DomainEvent
{
    public Guid EventId { get; init; } = eventid;

}
