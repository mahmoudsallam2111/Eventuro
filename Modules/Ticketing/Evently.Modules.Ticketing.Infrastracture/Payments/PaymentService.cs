﻿using Evently.Modules.Ticketing.Application.Abstractions.Payment;

namespace Evently.Modules.Ticketing.Infrastracture.Payments;
internal sealed class PaymentService : IPaymentService
{
    public Task<PaymentResponse> ChargeAsync(decimal amount, string currency)
    {
        return Task.FromResult(new PaymentResponse(Guid.NewGuid(), amount, currency));
    }

    public Task RefundAsync(Guid transactionId, decimal amount)
    {
        return Task.CompletedTask;
    }
}
