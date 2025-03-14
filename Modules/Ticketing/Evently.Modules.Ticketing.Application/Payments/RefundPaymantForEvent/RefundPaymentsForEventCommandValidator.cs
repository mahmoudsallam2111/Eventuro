﻿using FluentValidation;

namespace Evently.Modules.Ticketing.Application.Payments.RefundPaymantForEvent;
internal sealed class RefundPaymentsForEventCommandValidator : AbstractValidator<RefundPaymentsForEventCommand>
{
    public RefundPaymentsForEventCommandValidator()
    {
        RuleFor(c => c.EventId).NotEmpty();
    }
}
