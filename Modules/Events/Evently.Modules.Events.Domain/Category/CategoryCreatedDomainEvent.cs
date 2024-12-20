﻿using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Event;

namespace Evently.Modules.Events.Domain.Category;
public sealed class CategoryCreatedDomainEvent(Guid categoryId) : DomainEvent
{
    public Guid CategoryId { get; init; } = categoryId;
}
