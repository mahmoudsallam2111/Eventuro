using Evently.Modules.Events.Application.EventStatistics;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Evently.Modules.Events.Infrastructure.EventStatistics;

internal sealed class EventStatisticsRepository : IEventStatisticsRepository
{
    private readonly IMongoCollection<Application.EventStatistics.EventStatistics> _collection;

    public EventStatisticsRepository(IMongoClient mongoClient , IOptions<DocumentDbSettings> settings)
    {
        DocumentDbSettings documentDbSettings = settings.Value;
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(documentDbSettings.Database);
        _collection = mongoDatabase.GetCollection<Application.EventStatistics.EventStatistics>(documentDbSettings.EventStatistics);
    }
    public async Task<Application.EventStatistics.EventStatistics> GetAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        FilterDefinition<Application.EventStatistics.EventStatistics> filter =
            Builders<Application.EventStatistics.EventStatistics>.Filter.Eq(e => e.EventId, eventId);

        Application.EventStatistics.EventStatistics? eventStatistics =
            await _collection.Find(filter).SingleAsync(cancellationToken: cancellationToken);

        return eventStatistics;

    }

    public async Task InsertAsync(Application.EventStatistics.EventStatistics eventStatistics, CancellationToken cancellationToken = default)
    {
        var options = new InsertOneOptions();
        try
        {
           await _collection.InsertOneAsync(eventStatistics, options, cancellationToken);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task ReplaceAsync(Application.EventStatistics.EventStatistics eventStatistics, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
