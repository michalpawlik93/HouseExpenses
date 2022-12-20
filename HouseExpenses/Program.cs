using FluentValidation;
using HouseExpenses.Api.ExtensionMethods;
using HouseExpenses.Api.Mappers;
using HouseExpenses.Api.Middlewares;
using HouseExpenses.Api.Models;
using HouseExpenses.Api.Validators;
using HouseExpenses.Data.Service;
using HousExpenses.Domain.Exceptions;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataStore(builder.Environment);
builder.Services.AddServices();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

app.CreateDatabase();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();

app.UseExceptionHandler(exceptionHandlerApp
    => exceptionHandlerApp.UseExceptionHandlerMiddleware());


app.MapGet("/expenses/getAll", async (IExpensesService expensesService) =>
{
    var results = await expensesService.GetAllAsync();
    var serviceResponse = new ServiceResponse<IEnumerable<ExpenseDto>> { Data = results.Select(ExpenseMapper.MapToDto) };
    return Results.Ok(serviceResponse);
})
    .WithName("Get All")
    .WithOpenApi();

app.MapPost("/expenses", async (CreateExpenseDto model, IExpensesService repo) =>
{
    await repo.AddExpenseAsync(ExpenseMapper.MapToDao(model));
    return Results.Accepted(null, new ServiceResponse());
})
    .AddEndpointFilter<ValidatorFilter<CreateExpenseDto>>()
    .WithName("Add Exepense")
    .WithOpenApi();

app.MapDelete("/expenses/{id}", async (Guid id, IExpensesService repo) =>
{
    await repo.RemoveExpenseAsync(id);
    return Results.Accepted(null, new ServiceResponse());
});

app.Run();
