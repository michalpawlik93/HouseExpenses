using HouseExpenses.Data.Service;

namespace HouseExpenses.Api.ExtensionMethods;

public static class WebApplicationExtensionMethods
{
    public static void CreateDatabase(this WebApplication application)
    {
        using (var scope = application.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var expensesService = services.GetRequiredService<IExpensesService>();
            expensesService.CreateTheDatabaseAsync().GetAwaiter().GetResult();
        }
    }
}
