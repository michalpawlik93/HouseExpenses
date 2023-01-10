using HouseExpenses.Data.Configuration;
using HouseExpenses.Data.Models;
using HouseExpenses.Data.Services.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HouseExpenses.Data.Services;

public class ExpensesCoreApiService : IExpensesCoreApiService
{
    private readonly Container _container;
    private readonly ILogger _logger;
    public ExpensesCoreApiService(CosmosClient cosmosDbClient, ILogger<ExpensesCoreApiService> logger, IOptions<CosmosDbSettings> dbSettings)
    {
        _ = dbSettings.Value?.DatabaseName ?? throw new ArgumentNullException(nameof(dbSettings));
        _container = cosmosDbClient.GetContainer(dbSettings.Value.DatabaseName, CosmosDbContainers.EXPENSE_CONTAINER);
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<ExpenseDao>> GetByHouseId(Guid houseId)
    {
        try
        {
            var queryString = $"SELECT * FROM Expenses e JOIN j IN e.Jobs WHERE j.House.Id = '{houseId}'";
            var query = _container.GetItemQueryIterator<ExpenseDao>(new QueryDefinition(queryString));
            var results = new List<ExpenseDao>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(default, ex, $"Error retreving item from database. HouseId: {houseId}");
            throw;
        }
    }

    public async Task UpdateAsync(string id, ExpenseDao dao)
    {
        try
        {
            await _container.UpsertItemAsync(dao, new PartitionKey(id));
        }
        catch (Exception ex)
        {
            _logger.LogError(default, ex, $"Error retreving item from database. ExpenseId: {id}");
            throw;
        }
    }
    //https://code-maze.com/azure-cosmos-db-with-asp-net-core-web-api/
}
