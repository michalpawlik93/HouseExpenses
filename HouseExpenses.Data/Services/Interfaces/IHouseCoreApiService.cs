using HouseExpenses.Data.Models;

namespace HouseExpenses.Data.Services.Interfaces;

public interface IHouseCoreApiService
{
    Task AddAsync(HouseDao house);
}
