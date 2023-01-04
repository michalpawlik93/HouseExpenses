using HouseExpenses.Data.Services;
using HouseExpenses.Data.Services.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(HouseExpenses.Function.EventGrid.UpdateExpenses.Startup))]
namespace HouseExpenses.Function.EventGrid.UpdateExpenses
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddConfiguration(config);
            builder.Services.AddCosmosClient();
            builder.Services.AddSingleton<IHouseCoreApiService, HouseCoreApiService>();
            builder.Services.AddSingleton<IExpensesCoreApiService, ExpensesCoreApiService>();
        }
    }
}
