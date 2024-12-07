using Evently.Common.Application.Messaging;

namespace Evently.Modules.Events.Application.Events.GetEvents;
public sealed class GetEventsQuery : IQuery<IReadOnlyCollection<GetEventResponse>>
{
}
