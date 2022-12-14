using HouseExpenses.Api.ExtensionMethods;
using HouseExpenses.Data.Models;
using HouseExpenses.Data.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataStore();
builder.Services.AddServices();

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
    .WithName("HelloWorld")
    .WithOpenApi();

app.MapPost("/expenses", async (ExpenseDao model, IExpensesService repo) =>
{
    if(model == null)
    {
        await repo.AddExpenseAsync(new ExpenseDao { Id = Guid.NewGuid(), Name = "Foundation", Created = DateTime.UtcNow });     
    }
    await repo.AddExpenseAsync(model);
});


app.Run();
