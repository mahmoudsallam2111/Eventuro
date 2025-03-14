using Evently.Common.Application.Messaging;

namespace Evently.Modules.Ticketing.Application.Payments.RefundPaymant;
public sealed record RefundPaymentCommand(Guid PaymentId, decimal Amount) : ICommand;
