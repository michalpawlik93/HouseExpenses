using HouseExpenses.Data.Configuration;
using HouseExpenses.Data.Models;
using HouseExpenses.Data.Services.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace HouseExpenses.Data.Services;

public class HouseCoreApiService : IHouseCoreApiService
{
    private readonly Container _container;
    private readonly ILogger _logger;
    public HouseCoreApiService(CosmosClient cosmosDbClient, ILogger<ExpensesCoreApiService> logger,
        string databaseName)
    {
        _container = cosmosDbClient.GetContainer(databaseName, CosmosDbContainers.HOUSE_CONTAINER);
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task AddAsync(HouseDao house)
    {
        try
        {
            await _container.CreateItemAsync(house, new PartitionKey(house.Id.ToString()));
        }
        catch (Exception ex)
        {
            _logger.LogError(default, ex, $"Error adding house.");
            throw;
        }
    }

    public async Task<HouseDao> GetById(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<HouseDao>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (Exception ex)
        {
            _logger.LogError(default, ex, $"Error retreving item from database. Id: {id}");
            throw;
        }
    }
}
