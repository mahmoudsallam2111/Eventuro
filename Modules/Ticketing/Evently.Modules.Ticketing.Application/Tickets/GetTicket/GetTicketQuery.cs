using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Application.Messaging;

namespace Evently.Modules.Ticketing.Application.Tickets.GetTicket;
public sealed record GetTicketQuery(Guid TicketId) : IQuery<TicketResponse>;
