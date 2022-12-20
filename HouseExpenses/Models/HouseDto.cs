namespace HouseExpenses.Api.Models;

/// <summary>
/// DTO representing House
/// </summary>
public class HouseDto
{
    /// <summary>
    /// Rooms collection
    /// </summary>
    public List<RoomDto> Rooms { get; set; }
}
