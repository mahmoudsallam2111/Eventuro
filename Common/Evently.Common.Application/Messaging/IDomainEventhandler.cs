using Evently.Common.Domain;
using MediatR;

namespace Evently.Common.Application.Messaging;
public interface IDomainEventhandler<in T> : INotificationHandler<T>
   where T : IDomainEvent
{
}
