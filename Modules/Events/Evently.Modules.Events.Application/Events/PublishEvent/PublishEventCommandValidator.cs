using FluentValidation;

namespace Evently.Modules.Events.Application.Events.PublishEvent;
internal sealed class PublishEventCommandValidator : AbstractValidator<PublishEventCommand>
{
    public PublishEventCommandValidator()
    {
        RuleFor(e => e.EventId).NotEmpty()
                               .WithMessage("EventId Should Not Be Empty");
    }
}
