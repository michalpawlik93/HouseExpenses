using HouseExpenses.Data.Models;

namespace HouseExpenses.Data.Services.Interfaces;

public interface IExpensesService
{
    public Task CreateTheDatabaseAsync();
    public Task AddExpenseAsync(ExpenseDao expense);
    public Task<IEnumerable<ExpenseDao>> GetAllAsync();
    public Task RemoveExpenseAsync(Guid id);
}
