using FluentValidation;
using HouseExpenses.Api.ExtensionMethods;
using HouseExpenses.Api.Mappers;
using HouseExpenses.Api.Models;
using HouseExpenses.Api.Validators;
using HouseExpenses.Data.Service;
using HousExpenses.Domain.Exceptions;

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

app.MapGet("/expenses/getAll", async (IExpensesService expensesService) =>
    Results.Ok(await expensesService.GetAllAsync()))
        .WithName("Get All")
        .WithOpenApi();

app.MapPost("/expenses", async (CreateExpenseDto model, IExpensesService repo) =>
{
    await repo.AddExpenseAsync(CreateExpenseMapper.MapToDao(model));
})
    .AddEndpointFilter<ValidatorFilter<CreateExpenseDto>>()
    .WithName("Add Exepense")
    .WithOpenApi();

app.MapDelete("/expenses/{id}", async (Guid id, IExpensesService repo) =>
{
    try {
        await repo.RemoveExpenseAsync(id);
        return Results.Ok(id);
    }
    catch(ResourceNotFoundException)
    {
        return Results.NotFound();//move to midleware or filter
    }
});

app.Run();
