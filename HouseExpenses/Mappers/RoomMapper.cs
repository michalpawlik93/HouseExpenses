using HouseExpenses.Api.Models;
using HouseExpenses.Data.Models;

namespace HouseExpenses.Api.Mappers
{
    public static class RoomMapper
    {
        public static RoomDao MapToDao(RoomDto model) =>
            new()
            {
                XDimension = model.XDimension,
                YDimension = model.YDimension,
                HDimension = model.HDimension,
            };
    }
}
