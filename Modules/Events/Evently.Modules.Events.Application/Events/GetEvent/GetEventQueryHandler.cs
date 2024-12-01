using System.Data.Common;
using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using MediatR;
namespace Evently.Modules.Events.Application.Events.GetEvent;



internal sealed class GetEventQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IRequestHandler<GetEventQuery, EventResponse?>
{
    public async Task<EventResponse?> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
           $"""
              Select 
                      Id As {nameof(EventResponse.Id)},
                      title As {nameof(EventResponse.Title)},
                      description As {nameof(EventResponse.Description)},
                      location As {nameof(EventResponse.Location)},
                      starts_at_utc As {nameof(EventResponse.StartsAtUtc)},
                      ends_at_utc As {nameof(EventResponse.EndsAtUtc)},
                      status As {nameof(EventResponse.StartsAtUtc)}

             FROM events.events
             Where id = @EventId
            """;

        EventResponse? @event = await dbConnection.QuerySingleOrDefaultAsync(sql, request);
        return @event;
    }
}
