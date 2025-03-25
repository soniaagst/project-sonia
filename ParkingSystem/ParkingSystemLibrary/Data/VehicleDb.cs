using Microsoft.EntityFrameworkCore;

namespace ParkingSystemLibrary.Models;

public class VehicleDb : DbContext
{
    public VehicleDb(DbContextOptions options) : base(options) { }
    public DbSet<Vehicle> Vehicles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>()
            .HasKey(v => v.LicensePlate);
    }
}