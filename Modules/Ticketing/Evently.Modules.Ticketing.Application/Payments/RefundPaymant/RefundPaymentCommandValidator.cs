﻿using FluentValidation;

namespace Evently.Modules.Ticketing.Application.Payments.RefundPaymant;
internal sealed class RefundPaymentCommandValidator : AbstractValidator<RefundPaymentCommand>
{
    public RefundPaymentCommandValidator()
    {
        RuleFor(c => c.PaymentId).NotEmpty();
    }
}
