using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Archtecture.Tests.Abstractions;
public abstract class BaseTest
{
    protected const string UsersNamespace = "Evently.Modules.Users";
    protected const string UsersIntegrationEventsNamespace = "Evently.Modules.Users.IntegrationEvents";

    protected const string EventsNamespace = "Evently.Modules.Events";
    protected const string EventsIntegrationEventsNamespace = "Evently.Modules.Events.IntegrationEvents";
    protected const string EventsPublicApiNamespace = "Evently.Modules.Events.PublicApi";

    protected const string TicketingNamespace = "Evently.Modules.Ticketing";
    protected const string TicketingIntegrationEventsNamespace = "Evently.Modules.Ticketing.IntegrationEvents";
}
