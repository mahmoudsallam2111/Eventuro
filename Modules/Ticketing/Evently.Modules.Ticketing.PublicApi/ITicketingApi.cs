namespace Evently.Modules.Ticketing.PublicApi;

public interface ITicketingApi
{
    Task CreateCostomerAsync(Guid CustomerId, 
        string email,
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default);
}
