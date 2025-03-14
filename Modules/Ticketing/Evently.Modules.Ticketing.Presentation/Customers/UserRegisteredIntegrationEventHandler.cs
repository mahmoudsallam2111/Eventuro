using Evently.Modules.Ticketing.Application.Customers.CreateCustomer;
using Evently.Modules.Users.IntegrationEvents;
using MassTransit;
using MediatR;
using Evently.Common.Domain;
using Evently.Common.Application.Exceptions;

namespace Evently.Modules.Ticketing.Presentation.Customers;
/// <summary>
/// i define the consumer here in presentaion layer as i considered it as an input to out ticketing module
/// </summary>
/// <param name="sender"></param>
public sealed class UserRegisteredIntegrationEventHandler(ISender sender) : IConsumer<UserRegisteredIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
      Result result =   await sender.Send(new CreateCustomerCommand(context.Message.Id,
            context.Message.Email,
            context.Message.FirstName,
            context.Message.LastName));

        if(result.IsFailure)
        {
            throw new EventlyException(nameof(CreateCustomerCommand), result.Error);
        }
    }
}
