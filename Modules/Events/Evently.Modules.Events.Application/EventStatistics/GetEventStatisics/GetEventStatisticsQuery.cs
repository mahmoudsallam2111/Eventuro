using Evently.Common.Application.Messaging;

namespace Evently.Modules.Events.Application.EventStatistics.GetEventStatisics;

public sealed record GetEventStatisticsQuery(Guid EventId) : IQuery<EventStatistics>;
