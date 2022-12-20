using HouseExpenses.Api.Models;
using HouseExpenses.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace HouseExpenses.Api.Filters;

public class HttpGlobalExceptionFilter : IExceptionFilter
{
    private const string ApplicationJson = "application/json";
    private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public void OnException(ExceptionContext context)
    {
        var response = new ServiceResponse(context.Exception);
        context.Result = new ContentResult() {
            StatusCode = (int)HttpUtils.GetStatusCode(context.Exception),
            Content = JsonSerializer.Serialize(response, _serializerOptions),
            ContentType= ApplicationJson
        };
    }
}
