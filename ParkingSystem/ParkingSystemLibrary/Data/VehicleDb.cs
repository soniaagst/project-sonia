using Microsoft.EntityFrameworkCore;
using ParkingSystemLibrary.Models;

namespace ParkingSystemLibrary.Data;

public class VehicleDb : DbContext
{
    public VehicleDb(DbContextOptions<VehicleDb> options) : base(options) { }
    
    public DbSet<Vehicle> Vehicles { get; set; } = null!;

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