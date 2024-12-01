using Evently.Modules.Events.Domain.Event;

namespace Evently.Modules.Events.Domain.Abstractions;

public abstract class DomainEvent : IDomainEvent
{
    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccursAtUtc = DateTime.UtcNow;
    }

    protected DomainEvent(Guid id, DateTime ocuursAtUtc)
    {
        Id = id;
        OccursAtUtc = ocuursAtUtc;
    }

    public Guid Id { get; init; }
    public DateTime OccursAtUtc { get; init; }
}
