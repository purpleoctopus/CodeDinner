using System.Net;

namespace CodeDinner.API.Models;

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
}