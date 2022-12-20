using HouseExpenses.Api.Models;
using HouseExpenses.Data.Models;

namespace HouseExpenses.Api.Mappers;

public static class CreateExpenseMapper
{
    public static ExpenseDao MapToDao(CreateExpenseDto model) =>
        new ExpenseDao()
        {
            Id = Guid.NewGuid(),
            Name= model.Name,
            Jobs = model.Jobs.Select(JobMapper.MapToDao).ToList(),
        };
}
