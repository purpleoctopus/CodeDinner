using System.Net;

namespace CodeBreakfast.Common.Models;

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public List<ResponseError>? Errors { get; set; }
}