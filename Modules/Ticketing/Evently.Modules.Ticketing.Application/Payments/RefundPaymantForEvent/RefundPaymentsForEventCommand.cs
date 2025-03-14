using Evently.Common.Application.Messaging;

namespace Evently.Modules.Ticketing.Application.Payments.RefundPaymantForEvent;

public sealed record RefundPaymentsForEventCommand(Guid EventId) : ICommand;
