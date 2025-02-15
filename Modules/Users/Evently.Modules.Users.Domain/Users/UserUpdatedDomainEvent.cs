using Evently.Common.Domain;

namespace Evently.Modules.Users.Domain.Users;
public sealed class UserUpdatedDomainEvent(Guid id , string firstName , string lastName) : DomainEvent
{
    public Guid UserId { get; init; } = id;
    public string FirstName { get; init; } = firstName;
    public string LastName { get; init; } = lastName;

}
