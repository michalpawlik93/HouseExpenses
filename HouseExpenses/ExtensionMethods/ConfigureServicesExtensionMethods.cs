using HouseExpenses.Data;
using HouseExpenses.Data.Repository;
using HouseExpenses.Data.Service;
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
        public static void AddDataStore(this IServiceCollection services, IWebHostEnvironment env) 
        {
            //https://github.com/Azure-Samples/cosmos-db-nosql-dotnet-samples
            var accountKey = Environment.GetEnvironmentVariable("COSMOS_KEY");
            var accountEndpoint = Environment.GetEnvironmentVariable("COSMOS_ENDPOINT");
            var connectionString = $"AccountEndpoint={accountEndpoint};AccountKey={accountKey}";
            services.AddDbContext<ExpenseStoreContext>(options =>
                options.UseCosmos(
                    connectionString: connectionString,
                    databaseName: "ExpenseStoreDb",
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