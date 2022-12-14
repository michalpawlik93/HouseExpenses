namespace HouseExpenses.Data.Models;

public class JobDao : RootDao
{
    public string Name { get; set; }
    public List<JobUnitDao> JobsUnits { get; set; }
}
