using ICommand = Evently.Common.Application.Messaging.ICommand;

namespace Evently.Modules.Events.Application.Events.CancelEvent;
public sealed record CancelEventCommand(Guid EventId) : ICommand;
