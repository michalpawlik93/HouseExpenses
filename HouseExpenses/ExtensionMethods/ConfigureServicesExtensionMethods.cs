using HouseExpenses.Data;
using HouseExpenses.Data.Configuration;
using HouseExpenses.Data.Services;
using HouseExpenses.Data.Services.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace HouseExpenses.Api.ExtensionMethods
{
    /// <summary>
    /// Method to add custom setup methods
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ConfigureServicesExtensionMethods
    {
        public static void AddConfiguration(this IServiceCollection services)
        {
            services.Configure<CosmosDbSettings>(options => ConfigurationExtension.Configuration.GetSection("CosmosDbSettings").Bind(options));
        }

        public static void AddDataStore(this IServiceCollection services, IWebHostEnvironment env) 
        {
            var accountKey = Environment.GetEnvironmentVariable("COSMOS_KEY");
            var accountEndpoint = Environment.GetEnvironmentVariable("COSMOS_ENDPOINT");
            var configurationSection = ConfigurationExtension.Configuration.GetSection("CosmosDbSettings");
            var databaseName = configurationSection["DatabaseName"];
            var connectionString = $"AccountEndpoint={accountEndpoint};AccountKey={accountKey}";
            services.AddDbContext<ExpenseStoreContext>(options =>
                options.UseCosmos(
                    connectionString: connectionString,
                    databaseName,
                    (clientOptions) => {
                        if (env.IsDevelopment())
                        {
                            SwitchOffCertificate(clientOptions);
                        }
                    }));
        }

        public static void SwitchOffCertificate(CosmosDbContextOptionsBuilder clientOptions)
        {
            clientOptions.HttpClientFactory(() => {
#pragma warning disable S4830 // Server certificates should be verified during SSL/TLS connections
                HttpMessageHandler httpMessageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
#pragma warning restore S4830 // Server certificates should be verified during SSL/TLS connections

                return new HttpClient(httpMessageHandler);
            });
            clientOptions.ConnectionMode(ConnectionMode.Gateway);
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IExpensesService, ExpensesService>();
        }
    }
}