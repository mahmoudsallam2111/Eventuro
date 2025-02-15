namespace Evently.Modules.Events.PublicApi;

public interface IEventApi
{
    Task<GetTicketTypeResponse?> GetTicketTypeAsync(Guid ticketId , CancellationToken cancellationToken);
}
