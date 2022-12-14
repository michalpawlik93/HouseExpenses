namespace HouseExpenses.Data.Models;

public class ExpenseDao : RootDao
{
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public List<JobDao> Jobs { get; set; }
}
