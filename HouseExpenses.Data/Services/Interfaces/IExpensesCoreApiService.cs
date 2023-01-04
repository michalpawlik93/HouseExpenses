using HouseExpenses.Data.Models;

namespace HouseExpenses.Data.Services.Interfaces;

public interface IExpensesCoreApiService
{
    Task UpdateAsync(string id, ExpenseDao dao);
    Task<IEnumerable<ExpenseDao>> GetByHouseId(Guid houseId);
}
