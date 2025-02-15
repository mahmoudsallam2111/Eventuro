using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Application.TicketTypes.GetTicketType;

namespace Evently.Modules.Events.Application.TicketTypes.GetAllTicketTypes;
internal sealed class GetAllTicketTypesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetAllTicketTypesQuery, IReadOnlyCollection<GetTicketTypeResponse>>
{
    public async Task<Result<IReadOnlyCollection<GetTicketTypeResponse>>> Handle(GetAllTicketTypesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(GetTicketTypeResponse.Id)},
                 event_id AS {nameof(GetTicketTypeResponse.EventId)},
                 name AS {nameof(GetTicketTypeResponse.Name)},
                 price AS {nameof(GetTicketTypeResponse.Price)},
                 currency AS {nameof(GetTicketTypeResponse.Currency)},
                 quantity AS {nameof(GetTicketTypeResponse.Quantity)}
             FROM events.ticket_types
             """;

        List<GetTicketTypeResponse> ticketTypes =
            (await connection.QueryAsync<GetTicketTypeResponse>(sql, request)).AsList();

        return ticketTypes;
    }
}
