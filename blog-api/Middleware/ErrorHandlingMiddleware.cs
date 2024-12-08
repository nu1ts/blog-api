using System.Net;
using blog_api.Exceptions;
using blog_api.Models;

namespace blog_api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            EmailException emailEx => ((int)HttpStatusCode.BadRequest, emailEx.Message),
            _ => ((int)HttpStatusCode.InternalServerError, "An unexpected error occurred")
        };

        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsJsonAsync(new Response
        {
            Status = $"{statusCode} {Enum.GetName(typeof(HttpStatusCode), statusCode)}",
            Message = message
        });
    }
}