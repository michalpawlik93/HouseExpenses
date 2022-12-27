using HouseExpenses.Data.Models;
using HouseExpenses.Data.Services.Interfaces;
using Microsoft.Azure.Cosmos;

namespace HouseExpenses.Data.Services;

public class HouseCoreApiService : IHouseCoreApiService
{
    private Container _container;
    public HouseCoreApiService(
    CosmosClient cosmosDbClient,
    string databaseName,
    string containerName)
    {
        _container = cosmosDbClient.GetContainer(databaseName, containerName);
    }

    //https://code-maze.com/azure-cosmos-db-with-asp-net-core-web-api/
    public async Task AddAsync(HouseDao house)
    {
        await _container.CreateItemAsync(house, new PartitionKey(house.Id.ToString()));
    }
}
