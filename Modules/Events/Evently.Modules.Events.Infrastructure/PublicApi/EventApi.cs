using Evently.Modules.Events.Application.TicketTypes.GetTicketType;
using Evently.Modules.Events.PublicApi;
using MediatR;
using GetTicketTypeResponse = Evently.Modules.Events.PublicApi.GetTicketTypeResponse;

namespace Evently.Modules.Events.Infrastructure.PublicApi;
internal sealed class EventApi(ISender sender)
    : IEventApi
{
    public async Task<GetTicketTypeResponse?> GetTicketTypeAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        Common.Domain.Result<Application.TicketTypes.GetTicketType.GetTicketTypeResponse> result = await sender.Send(new GetTicketTypeQuery(ticketId), cancellationToken);
        if (result.IsFailure)
        {
            return null;
        }

        return new GetTicketTypeResponse(result.Value.Id,
            result.Value.EventId,
            result.Value.Name,
            result.Value.Price,
            result.Value.Currency,
            result.Value.Quantity);
    }
}
