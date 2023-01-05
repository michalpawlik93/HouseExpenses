using HouseExpenses.Api.Mappers;
using HouseExpenses.Api.Models;
using HouseExpenses.Api.Validators;
using HouseExpenses.Data.Services.Interfaces;

namespace HouseExpenses.Api.ExtensionMethods;

public static class ExpensesModule
{
    public static void AddExpensesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/expenses/getAll", async (IExpensesService expensesService) =>
        {
            var results = await expensesService.GetAllAsync();
            var serviceResponse = new ServiceResponse<IEnumerable<ExpenseDto>> { Data = results.Select(ExpenseMapper.MapToDto) };
            return Results.Ok(serviceResponse);
        })
        .WithName("Get All")
        .WithOpenApi();

        app.MapPost("/expenses", async (CreateExpenseDto model, IExpensesService expensesService) =>
        {
            await expensesService.AddExpenseAsync(ExpenseMapper.MapToDao(model));
            return Results.Accepted(null, new ServiceResponse());
        })
            .AddEndpointFilter<ValidatorFilter<CreateExpenseDto>>()
            .WithName("Add Exepense")
            .WithOpenApi();

        app.MapDelete("/expenses/{id}", async (Guid id, IExpensesService expensesService) =>
        {
            await expensesService.RemoveExpenseAsync(id);
            return Results.Accepted(null, new ServiceResponse());
        });
    }
}
