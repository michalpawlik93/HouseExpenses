namespace HouseExpenses.Api.ExtensionMethods;

public static class ConfigurationExtension
{
    public static IConfiguration Configuration
    {
        get
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfiguration config = builder.Build();
            return config;
        }
    }
}
