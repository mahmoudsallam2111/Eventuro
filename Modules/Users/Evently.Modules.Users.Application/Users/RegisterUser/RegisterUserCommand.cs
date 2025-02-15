using Evently.Common.Application.Messaging;

namespace Evently.Modules.Users.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(string email , string firstName ,  string lastName ) : ICommand<Guid>
{
}
