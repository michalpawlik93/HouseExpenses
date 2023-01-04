using HouseExpenses.Data.Configuration;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HouseExpenses.Function.EventGrid.UpdateExpenses;

public static class ExtensionMethod
{
    public static void AddConfiguration(this IServiceCollection services, IConfigurationRoot config)
    {
        services.Configure<CosmosDbSettings>(options => config.GetSection("CosmosDbSettings").Bind(options));
    }
    public static void AddCosmosClient(this IServiceCollection services)
    {
        services.AddSingleton((s) => {
            var accountEndpoint = Environment.GetEnvironmentVariable("COSMOS_ENDPOINT");
            if (string.IsNullOrEmpty(accountEndpoint))
            {
                throw new ArgumentException("Cosmos account endpoint not provided");
            }
            var accountKey = Environment.GetEnvironmentVariable("COSMOS_KEY");
            if (string.IsNullOrEmpty(accountKey))
            {
                throw new ArgumentException("Cosmos account key not provided");
            }

            CosmosClientBuilder configurationBuilder = new CosmosClientBuilder(accountEndpoint, accountKey);
            return configurationBuilder
                    .Build();
        });
    }
}
