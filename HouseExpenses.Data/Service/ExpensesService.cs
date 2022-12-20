using HouseExpenses.Data.Models;
using HouseExpenses.Data.Service;
using HousExpenses.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseExpenses.Data.Repository;

public class ExpensesService : IExpensesService
{
    private readonly ILogger _logger;
    private readonly ExpenseStoreContext _context;
    public ExpensesService(ExpenseStoreContext expenseStoreContext, ILogger<ExpensesService> logger)
    {
        _context = expenseStoreContext ?? throw new ArgumentNullException(nameof(expenseStoreContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task CreateTheDatabaseAsync()
    {
        try
        {
            var created = await _context.Database.EnsureCreatedAsync();
            if (created)
            {
                _logger.LogInformation("Database created.");
            }
            else
            {
                _logger.LogInformation("Database already exists.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(default, ex, "Error when accessing database.");
        }
    }

    public async Task AddExpenseAsync(ExpenseDao expense)
    {
        try
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Expanse created.");
        }
        catch (Exception ex)
        {
            _logger.LogError(default, ex, "Error when adding to database.");
            throw;
        }
    }

    public async Task RemoveExpenseAsync(Guid id)
    {
        try
        {
            var expense = await _context.Expenses.FindAsync(id);
            _ = expense ?? throw new ResourceNotFoundException("Expnese not found");
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Expanse created.");
        }
        catch (Exception ex)
        {
            _logger.LogError(default, ex, "Error when removing from database.");
            throw;
        }
    }

    public async Task<IEnumerable<ExpenseDao>> GetAllAsync()
    {
        try
        {
            var results = await _context.Expenses.ToListAsync();
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(default, ex, "Error when reading expenses.");
            throw;
        }
    }
}

//https://csharp.christiannagel.com/2018/09/05/efcorecosmos/
