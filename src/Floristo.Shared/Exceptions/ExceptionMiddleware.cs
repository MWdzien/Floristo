using System.Text.Json;
using Floristo.SharedAbstractions.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Floristo.Shared.Exceptions;

internal sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (FloristoException e)
        {
            context.Response.StatusCode = e.StatusCode;
            context.Response.ContentType = "application/json";
             
            var errorCode = ToUnderscoreCase(e.GetType().Name).Replace("Exception", string.Empty);
            var json = JsonSerializer.Serialize(new {ErrorCode = errorCode, e.Message});
            
            await context.Response.WriteAsync(json);
        }
    }
    
    public static string ToUnderscoreCase(string value)
        => string.Concat((value ?? string.Empty).Select((x, i) => i > 0 && char.IsUpper(x) && !char.IsUpper(value[i-1]) ? $"_{x}" : x.ToString())).ToLower();
}