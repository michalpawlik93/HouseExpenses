namespace HouseExpenses.Data.Models;

public class ExpenseDao : RootDao
{
    public int Name { get; set; }
    public DateTime Created { get; set; }
    public List<JobDao> Jobs { get; set; }
}
