using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Event;

namespace Evently.Modules.Events.Domain.Category;
public sealed class CategoryNameChangedDomainEvent(Guid categoryId, string name) : DomainEvent
{
    public Guid CategoryId { get; init; } = categoryId;

    public string Name { get; init; } = name;
}
