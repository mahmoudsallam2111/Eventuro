using Evently.Modules.Events.Domain.Event;
using Evently.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Infrastructure.Event;
internal sealed class EventRepository(EventsDbContext dbContext) : IEventRepository
{
    public async Task<Domain.Event.Event?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Events.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }
    public void InsertEvent(Domain.Event.Event @event)
    {
        dbContext.Add(@event);
    }
}
