namespace HouseExpenses.Api.Models;

/// <summary>
/// DTO representing House
/// </summary>
public class HouseDto
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Rooms collection
    /// </summary>
    public List<RoomDto> Rooms { get; set; }
}
