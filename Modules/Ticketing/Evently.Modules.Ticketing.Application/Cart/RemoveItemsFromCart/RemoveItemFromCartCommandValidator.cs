﻿using FluentValidation;

namespace Evently.Modules.Ticketing.Application.Cart.RemoveItemsFromCart;
internal sealed class RemoveItemFromCartCommandValidator : AbstractValidator<RemoveItemFromCartCommand>
{
    public RemoveItemFromCartCommandValidator()
    {
        RuleFor(c => c.CustomerId).NotEmpty();
        RuleFor(c => c.TicketTypeId).NotEmpty();
    }
}
