using HouseExpenses.Api.Models;
using HouseExpenses.Data.Models;

namespace HouseExpenses.Api.Mappers;

public static class ExpenseMapper
{
    public static ExpenseDto MapToDto(ExpenseDao dao) =>
        new()
        {
            Id = dao.Id,
            Name = dao.Name,
            Jobs = dao.Jobs.Select(JobMapper.MapToDto).ToList(),
        };

    public static ExpenseDao MapToDao(CreateExpenseDto model) =>
        new()
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Jobs = model.Jobs.Select(JobMapper.MapToDao).ToList(),
        };

    public static ExpenseDao MapToDao(ExpenseDto model) =>
        new()
        {
            Id = model.Id,
            Name = model.Name,
            Jobs = model.Jobs.Select(JobMapper.MapToDao).ToList(),
        };
}
