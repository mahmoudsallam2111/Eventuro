using FluentValidation;

namespace Evently.Modules.Ticketing.Application.Cart.ClearCart;
internal sealed class ClearCartCommandValidator : AbstractValidator<ClearCartCommand>
{
    public ClearCartCommandValidator()
    {
        RuleFor(c => c.CustomerId).NotEmpty();
    }
}
