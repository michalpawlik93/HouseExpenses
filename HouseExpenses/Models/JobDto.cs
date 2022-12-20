namespace HouseExpenses.Api.Models;

/// <summary>
/// DTO representing Job
/// </summary>
public class JobDto
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Type
    /// </summary>
    public string Type { get; set; }
    public HouseDto House { get; set; }
}
