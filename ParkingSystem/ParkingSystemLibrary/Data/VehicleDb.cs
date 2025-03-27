using Microsoft.EntityFrameworkCore;
using ParkingSystemLibrary.Models;

namespace ParkingSystemLibrary.Data;

public class ParkingDb : DbContext
{
    public ParkingDb(DbContextOptions<ParkingDb> options) : base(options) { }
    
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    // public DbSet<User> Users {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>()
            .HasKey(v => v.LicensePlate);
        modelBuilder.Entity<Vehicle>()
            .Property(v => v.Type);
        modelBuilder.Entity<Vehicle>()
            .Property(v => v.Owner);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=parking.db");
        }
    }
}