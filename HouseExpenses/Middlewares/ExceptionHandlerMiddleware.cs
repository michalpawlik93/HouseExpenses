using HouseExpenses.Api.Models;
using HouseExpenses.Api.Utils;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace HouseExpenses.Api.Middlewares;

/// <summary>
/// Middleware for custom exception handling
/// </summary>
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApplicationJson = "application/json";
    private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };


    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var exHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        if(exHandlerFeature?.Error != null)
        {
            context.Response.StatusCode = (int)HttpUtils.GetStatusCode(exHandlerFeature.Error);
            context.Response.ContentType = ApplicationJson;
            var response = new ServiceResponse(exHandlerFeature.Error);
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, _serializerOptions));
            return;
        }
        await _next(context);
    }
}

public static class ExceptionHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
