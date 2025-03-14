using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Application.Messaging;
using Evently.Modules.Ticketing.Application.Orders.GetOrder;

namespace Evently.Modules.Ticketing.Application.Orders.GetOrders;
public sealed record GetOrdersQuery(Guid CustomerId) : IQuery<IReadOnlyCollection<OrderResponse>>;
