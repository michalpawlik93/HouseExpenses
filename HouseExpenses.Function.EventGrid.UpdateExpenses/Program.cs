using HouseExpenses.Data.Services;
using HouseExpenses.Data.Services.Interfaces;
using HouseExpenses.Function.EventGrid.UpdateExpenses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((_, services) =>
    {
        services.AddConfiguration();
        services.AddCosmosClient();
        services.AddSingleton<IHouseCoreApiService, HouseCoreApiService>();
        services.AddSingleton<IExpensesCoreApiService, ExpensesCoreApiService>();
     })
    .Build();

host.Run();
