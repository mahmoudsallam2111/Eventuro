﻿using Evently.Modules.Events.Application.Abstractions.Messaging;

namespace Evently.Modules.Events.Application.Categories.CreateCategory;
public sealed record CreateCategoryCommand (string categoryName) : ICommand<Guid>;
