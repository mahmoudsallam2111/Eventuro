using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Application.TicketTypes.GetTicketType;

namespace Evently.Modules.Events.Application.TicketTypes.GetAllTicketTypes;
public sealed record GetAllTicketTypesQuery : IQuery<IReadOnlyCollection<GetTicketTypeResponse>>;

