using NetkServer.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace NetkServer.Database;

public class EntryContext : DbContext
{
    public EntryContext()
    {
        Database.EnsureCreated();
    }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Position> Positions { get; set; }

    public DbSet<EnterExitHistory> EnterExitHistory { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-6T38K7E;Database=EmployeeAccounting;Trusted_Connection=True;");
    }
}