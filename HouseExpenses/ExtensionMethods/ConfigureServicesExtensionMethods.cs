using HouseExpenses.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using HouseExpenses.Data.Service;
using HouseExpenses.Data.Repository;

namespace HouseExpenses.Api.ExtensionMethods
{
    /// <summary>
    /// Method to add custom setup methods
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ConfigureServicesExtensionMethods
    {
        public static void AddDataStore(this IServiceCollection services) 
        {
            //move conection string to settings
            services.AddDbContext<ExpenseStoreContext>(options =>
                options.UseCosmos(
                    connectionString: "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                    databaseName: "ExpenseStoreDb"));
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IExpensesService, ExpensesService>();
        }
    }
}
