namespace HouseExpenses.Data.Models;

public class JobUnitDao : RootDao
{
    public string Name { get; set; }
    public JobDao Job { get; set; }
}
