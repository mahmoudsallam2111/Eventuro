namespace Evently.Common.Application.EventBus;

public abstract class IntegrationEvent: IIntegrationEvent
{
    protected IntegrationEvent(Guid id, DateTime occurRedOnUtc)
    {
        Id = id;
        OccurRedOnUtc = occurRedOnUtc;
    }

    public Guid Id { get; init; }
   public DateTime OccurRedOnUtc { get; init; }
}
