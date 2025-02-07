using Evently.Common.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Common.Infrastructure.Interceptors;
public class PublishDomainEventsInterceptors(IServiceScopeFactory serviceScopeFactory) : SaveChangesInterceptor
{
    /// <summary>
    /// The overridden methods (SavedChangesAsync) are called after the database changes are successfully committed.
    /// 
    /// <returns></returns>
    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            await PublishDomainEventsAsync(eventData.Context);
        }
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEventsAsync(DbContext context)
    {
        IEnumerable<IDomainEvent> domainEvents = context
                                .ChangeTracker
                                .Entries<Entity>()
                                .Select(e => e.Entity)
                                .SelectMany(entity =>
                                {
                                    IReadOnlyCollection<IDomainEvent> domainEvents = entity.DomainEvents;
                                    entity.ClearDomainEvents();
                                    return domainEvents;
                                }).ToList();

        using IServiceScope scope = serviceScopeFactory.CreateScope();

        IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }

    }
}
