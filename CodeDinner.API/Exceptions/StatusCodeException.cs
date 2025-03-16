using System.Net;

namespace CodeDinner.API.Exceptions;

public class StatusCodeException : Exception
{
    public StatusCodeException(HttpStatusCode httpStatusCode)
    {
        StatusCode = httpStatusCode;
    }
    
    public StatusCodeException(HttpStatusCode httpStatusCode, string message) : base(message)
    {
        StatusCode = httpStatusCode;
    }
    
    public HttpStatusCode? StatusCode { get; set; } = HttpStatusCode.InternalServerError;
}