namespace Evently.Modules.Events.Domain.Event;
public interface IEventRepository
{
    Task<Domain.Event.Event?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    void InsertEvent(Event @event);
}
