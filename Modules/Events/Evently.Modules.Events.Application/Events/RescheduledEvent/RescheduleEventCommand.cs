﻿using Evently.Common.Application.Messaging;

namespace Evently.Modules.Events.Application.Events.RescheduledEvent;
public sealed record RescheduleEventCommand(Guid EventId, DateTime StartsAtUtc, DateTime? EndsAtUtc) : ICommand
{
}
