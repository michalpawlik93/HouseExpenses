using HouseExpenses.Api.Models;
using HouseExpenses.Data.Models;

namespace HouseExpenses.Api.Mappers;

public static class JobMapper
{
    public static JobDao MapToDao(JobDto model) =>
        new()
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Type= model.Type,
            House = HouseMapper.MapToDao(model.House),
            JobsUnits = null
        };
}
