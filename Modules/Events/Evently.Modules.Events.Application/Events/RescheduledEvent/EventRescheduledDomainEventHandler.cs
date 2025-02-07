using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Domain.Event;

namespace Evently.Modules.Events.Application.Events.RescheduledEvent;
internal sealed class EventRescheduledDomainEventHandler : IDomainEventhandler<EventRescheduledDomainEvent>
{
    public Task Handle(EventRescheduledDomainEvent domainEventDate, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
