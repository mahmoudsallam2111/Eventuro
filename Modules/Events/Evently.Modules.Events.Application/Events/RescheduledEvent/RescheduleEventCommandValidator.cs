using FluentValidation;

namespace Evently.Modules.Events.Application.Events.RescheduledEvent;
internal sealed class RescheduleEventCommandValidator : AbstractValidator<RescheduleEventCommand>
{
    public RescheduleEventCommandValidator()
    {
        RuleFor(e=>e.EventId).NotEmpty();
        RuleFor(e=>e.StartsAtUtc).NotEmpty();
        RuleFor(e => e.EndsAtUtc).Must((cmd, end) => end > cmd.StartsAtUtc).When(c => c.EndsAtUtc.HasValue);
    }
}
