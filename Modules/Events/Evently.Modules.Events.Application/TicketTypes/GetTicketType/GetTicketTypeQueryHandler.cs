using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.TicketTypes;
using System.Data.Common;

namespace Evently.Modules.Events.Application.TicketTypes.GetTicketType;
internal sealed class GetTicketTypeQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetTicketTypeQuery, GetTicketTypeResponse>
{
    public async Task<Result<GetTicketTypeResponse>> Handle(
        GetTicketTypeQuery request,
        CancellationToken cancellationToken)
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
             WHERE id = @TicketTypeId
             """;

        GetTicketTypeResponse? ticketType =
            await connection.QuerySingleOrDefaultAsync<GetTicketTypeResponse>(sql, request);

        if (ticketType is null)
        {
            return Result.Failure<GetTicketTypeResponse>(TicketTypeErrors.NotFound(request.TicketTypeId));
        }

        return ticketType;
    }
}
