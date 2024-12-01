using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Event;
using MediatR;

namespace Evently.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateEventCommand, Guid>
{
    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var @event = Domain.Event.Event.Create(
           request.Title,
           request.Description,
           request.Location,
           request.StartsAtUtc,
           request.EndsAtUtc);

        eventRepository.InsertEvent(@event);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return @event.Id;

    }
}
