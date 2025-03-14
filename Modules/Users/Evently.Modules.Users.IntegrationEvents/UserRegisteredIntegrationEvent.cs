﻿using Evently.Common.Application.EventBus;

namespace Evently.Modules.Users.IntegrationEvents;

public sealed class UserRegisteredIntegrationEvent : IntegrationEvent
{
    public UserRegisteredIntegrationEvent(
        Guid id,
        DateTime occurRedOnUtc,
        Guid userId,
        string email,
        string firstName,
        string lastName) : base(id, occurRedOnUtc)
    {
        UserId = userId;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public Guid UserId { get; init; }
    public string Email { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }

}
