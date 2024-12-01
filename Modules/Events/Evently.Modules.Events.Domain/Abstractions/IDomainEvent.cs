namespace Evently.Modules.Events.Domain.Event;

public interface IDomainEvent
{
     Guid Id { get;}
     DateTime OccursAtUtc { get;}
}
