using ParkingSystemLibrary.Models;
using ParkingSystemLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace ParkingSystemLibrary.Repositories;

public class VehicleRepository 
{
    private VehicleDb _vehicleDb;

    public VehicleRepository(VehicleDb vehicleDb)
    {
        _vehicleDb = vehicleDb;
    }

    public async Task<List<Vehicle>> GetVehiclesAsync()
    {
        return await _vehicleDb.Vehicles.ToListAsync();
    }

    public async Task<List<Vehicle>> GetVehiclesByOwnerAsync(string owner)
    {
        return await _vehicleDb.Vehicles.Where(vhcl => vhcl.Owner == owner).ToListAsync();
    }

    public async Task<Vehicle?> GetVehicleByLicensePlateAsync(string licensePlate)
    {
        return await _vehicleDb.Vehicles.FirstOrDefaultAsync(vhcl => vhcl.LicensePlate == licensePlate);
    }

    public async Task AddVehicleAsync(Vehicle vehicle)
    {
        _vehicleDb.Vehicles.Add(vehicle);
        await _vehicleDb.SaveChangesAsync();
    }

    public async Task RemoveVehicleAsync(Vehicle vehicle)
    {
        _vehicleDb.Vehicles.Remove(vehicle);
        await _vehicleDb.SaveChangesAsync();
    }
}