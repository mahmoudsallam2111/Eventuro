using Evently.Common.Domain;

namespace Evently.Common.Application.Exceptions;
public class EventlyException : Exception
{
    public EventlyException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application Exception" , innerException)
    {
        Error = error;
        RequestName = requestName;
    }

    public Error? Error { get;}

    public string RequestName { get;}
}
