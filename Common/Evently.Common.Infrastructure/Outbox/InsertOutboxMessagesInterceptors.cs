using Evently.Common.Domain;
using Evently.Common.Infrastructure.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Evently.Common.Infrastructure.Outbox;
public class InsertOutboxMessagesInterceptors : SaveChangesInterceptor
{
    /// <summary>
    /// The overridden methods (SavedChangesAsync) are called after the database changes are successfully committed.
    /// this class first was called PublishDomainEventsInterceptors and used to publish messages directlt but now it refactored to
    /// implement Outbox pattern
    /// <returns></returns>


    // this is called before save changes executed
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            InsertOutboxMessagesAsync(eventData.Context);
        }
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    private static void InsertOutboxMessagesAsync(DbContext context)
    {
        var outboxMessages = context
             .ChangeTracker
             .Entries<Entity>()
             .Select(entry => entry.Entity)
             .SelectMany(entity =>
             {
                 IReadOnlyCollection<IDomainEvent> domainEvents = entity.DomainEvents;

                 entity.ClearDomainEvents();

                 return domainEvents;
             })
             .Select(domainEvent => new OutboxMessage
             {
                 Id = domainEvent.Id,
                 Type = domainEvent.GetType().Name,
                 Content = JsonConvert.SerializeObject(domainEvent, SerializerSettings.Instance),
                 OccurredOnUtc = domainEvent.OccurredOnUtc
             })
             .ToList();

        context.Set<OutboxMessage>().AddRange(outboxMessages);

    }
}
