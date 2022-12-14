using HouseExpenses.Api.ExtensionMethods;
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

app.MapGet("/GetAllExpenses", () => "Hello Wolr")
    .WithName("HelloWorld")
    .WithOpenApi();


app.Run();
