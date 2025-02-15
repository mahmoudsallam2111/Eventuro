using Evently.Common.Domain;

namespace Evently.Modules.Users.Domain.Users;
public sealed class NewUserRegisteredDomainEvent(Guid id) : DomainEvent
{
    public Guid UserId { get; init; } = id;
}
