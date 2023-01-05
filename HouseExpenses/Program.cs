using FluentValidation;
using HouseExpenses.Api.ExtensionMethods;
using HouseExpenses.Api.Middlewares;

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

app.AddExpensesEndpoints();

app.Run();
