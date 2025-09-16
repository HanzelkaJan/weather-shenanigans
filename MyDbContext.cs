using Microsoft.EntityFrameworkCore;

namespace WeatherShenanigans;

public class MyDbContext : DbContext
{
    public DbSet<WeatherObservation> WeatherObservation { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string folder = Directory.GetCurrentDirectory();
        string dbName = "TestDB.db";
        string path = Path.Combine(folder, dbName);
        optionsBuilder.UseSqlite($"data source={path}");
        //Console.WriteLine(path);
        //optionsBuilder.UseInMemoryDatabase("DummyDb");
    }
}