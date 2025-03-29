namespace Evently.Modules.Users.Infrastructure.Outbox;
internal sealed class OutboxOptions
{
    public int IntervalInSeconds { get; init; }   // how often we wanna run the job

    public int BatchSize { get; init; }
}
