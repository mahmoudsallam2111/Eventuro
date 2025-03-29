using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Application.EventStatistics;
using Evently.Modules.Events.Domain.Event;

namespace Evently.Modules.Events.Application.Events.CreateEvent;

public class EventCreatedDomainEventHandler : IDomainEventhandler<EventCreatedDomainEvent>
{
    private readonly IEventStatisticsRepository _eventStatisticsRepository;
    private readonly IEventRepository _eventRepository;

    public EventCreatedDomainEventHandler(IEventStatisticsRepository eventStatisticsRepository , 
        IEventRepository eventRepository)
    {
        _eventStatisticsRepository = eventStatisticsRepository;
        _eventRepository = eventRepository;
    }
    public async Task Handle(EventCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Event? @event = await _eventRepository.GetAsync(notification.EventId , cancellationToken);

        if (@event == null)
        {
            throw new InvalidOperationException($"Event with ID {notification.EventId} not found.");
        }

        var eventStatistics = EventStatistics.EventStatistics.Create(
            @event.Id,
            @event.Title,
            @event.Description,
            @event.Location,
            @event.StartsAtUtc,
            @event.EndsAtUtc
        );
       await _eventStatisticsRepository.InsertAsync(eventStatistics, cancellationToken);
    }
}
