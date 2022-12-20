using HouseExpenses.Api.Models;
using HouseExpenses.Data.Models;

namespace HouseExpenses.Api.Mappers;

public static class HouseMapper
{
    public static HouseDao MapToDao(HouseDto model) =>
        new()
        {
            Id = Guid.NewGuid(),
            Rooms = model.Rooms.Select(RoomMapper.MapToDao).ToList(),
        };

    public static HouseDto MapToDto(HouseDao dao) =>
        new()
        {
            Id = dao.Id,
            Rooms = dao.Rooms.Select(RoomMapper.MapToDto).ToList(),
        };
}
