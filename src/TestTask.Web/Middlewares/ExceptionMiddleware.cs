using System.Diagnostics;
using TestTask.Application.Models;

namespace TestTask.Web.Middlewares;

public class ExceptionMiddleware
{
    private RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BadHttpRequestException ex)
        {
            Debug.Print(ex.Message);
            
            var error = new Error(ex.Message, "wrong.input", StatusCodes.Status400BadRequest);
            var envelope = new Envelope(null, [error]);

            await MakeResponse(context, "application/json", StatusCodes.Status400BadRequest, envelope);
        }
        catch (Exception ex)
        {
            var error = new Error(ex.Message, "excepted.error", StatusCodes.Status500InternalServerError);
            var envelope = new Envelope(null, [error]);

            await MakeResponse(context, "application/json", StatusCodes.Status400BadRequest, envelope);
        }
    }

    private async Task MakeResponse(HttpContext context, string contentType, int statusCode, Envelope envelope)
    {
        context.Response.ContentType = contentType;
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(envelope);
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}