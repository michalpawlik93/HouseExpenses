using HouseExpenses.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseExpenses.Data;

public class ExpenseStoreContext : DbContext
{
    public ExpenseStoreContext(DbContextOptions<ExpenseStoreContext> options)
       : base(options) { }
    public DbSet<ExpenseDao> Expenses { get; set; }
    public DbSet<HouseDao> House { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExpenseDao>()
            .HasNoDiscriminator()//one type stored in the table, so there is no need for a discriminator
            .ToContainer("ExpanseContainer")
            .HasPartitionKey(e => e.Id)
            .HasKey(e => e.Id);


        modelBuilder.Entity<ExpenseDao>()
            .OwnsMany(e => e.Jobs)
            .OwnsMany(e=>e.JobsUnits);

        modelBuilder.Entity<HouseDao>()
            .HasNoDiscriminator()//one type stored in the table, so there is no need for a discriminator
            .ToContainer("HouseContainer")
            .HasPartitionKey(e => e.Id)
            .HasKey(e => e.Id);

        modelBuilder.Entity<HouseDao>()
             .OwnsMany(e => e.Rooms);
    }
}

//https://www.youtube.com/watch?v=CncakcC-Shc
//https://csharp.christiannagel.com/2018/09/05/efcorecosmos/
//https://libertus.dev/posts/connect-to-cosmosbd-emulator-on-docker/part1/#inst-cert
