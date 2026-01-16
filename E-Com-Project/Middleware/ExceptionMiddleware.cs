using ECom.Application.Common;
using System.Net;
using System.Text.Json;

namespace ECom.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            await WriteError(context, 404, ex.Message);
        }
        catch (ArgumentException ex)
        {
            await WriteError(context, 400, ex.Message);
        }
        catch (Exception ex)
        {
            await WriteError(context, 500, "Internal Server Error");
        }
        
    }

    private static Task WriteError(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = new ApiResponse<object>(
            statusCode,
            message,
            null
        );

        return context.Response.WriteAsync(
            JsonSerializer.Serialize(response)
        );
    }

}
