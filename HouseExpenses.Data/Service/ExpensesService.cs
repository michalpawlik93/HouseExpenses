using HouseExpenses.Data.Service;

namespace HouseExpenses.Data.Repository;

public class ExpensesService : IExpensesService
{
    private readonly ExpenseStoreContext _context;
    public ExpensesService(ExpenseStoreContext expenseStoreContext)
    {
        _context = expenseStoreContext ?? throw new ArgumentNullException(nameof(expenseStoreContext));
    }

    public async Task CreateTheDatabaseAsync() =>
        await _context.Database.EnsureCreatedAsync();
}

//https://csharp.christiannagel.com/2018/09/05/efcorecosmos/
