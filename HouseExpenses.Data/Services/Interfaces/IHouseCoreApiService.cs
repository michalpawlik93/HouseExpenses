using HouseExpenses.Data.Models;

namespace HouseExpenses.Data.Services.Interfaces;

public interface IHouseCoreApiService
{
    Task<HouseDao> GetById(string id);
    Task AddAsync(HouseDao house);
}
