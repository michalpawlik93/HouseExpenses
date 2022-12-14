using HouseExpenses.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseExpenses.Data;

public class ExpenseStoreContext : DbContext
{
    public ExpenseStoreContext(DbContextOptions<ExpenseStoreContext> options)
       : base(options) { }
    public DbSet<ExpenseDao> Expenses { get; set; }
    public DbSet<JobDao> Jobs { get; set; }
    public DbSet<JobUnitDao> JobUnits { get; set; }
}

//https://www.youtube.com/watch?v=CncakcC-Shc
//https://csharp.christiannagel.com/2018/09/05/efcorecosmos/
//https://libertus.dev/posts/connect-to-cosmosbd-emulator-on-docker/part1/#inst-cert
