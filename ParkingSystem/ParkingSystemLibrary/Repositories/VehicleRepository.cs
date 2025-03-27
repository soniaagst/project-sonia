using ParkingSystemLibrary.Models;
using ParkingSystemLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace ParkingSystemLibrary.Repositories;

public class VehicleRepository 
{
    private ParkingDb _parkingDb;

    public VehicleRepository(ParkingDb vehicleDb)
    {
        _parkingDb = vehicleDb;
    }

    public async Task<List<Vehicle>> GetVehiclesAsync()
    {
        return await _parkingDb.Vehicles.ToListAsync();
    }

    public async Task<List<Vehicle>> GetVehiclesByOwnerAsync(string owner)
    {
        return await _parkingDb.Vehicles.Where(vhcl => vhcl.Owner == owner).ToListAsync();
    }

    public async Task<Vehicle?> GetVehicleByLicensePlateAsync(string licensePlate)
    {
        return await _parkingDb.Vehicles.FirstOrDefaultAsync(vhcl => vhcl.LicensePlate == licensePlate);
    }

    public async Task AddVehicleAsync(Vehicle vehicle)
    {
        _parkingDb.Vehicles.Add(vehicle);
        await _parkingDb.SaveChangesAsync();
    }

    public async Task UpdateVehicleOwnerAsync(Vehicle vehicle, string newOwner)
    {
        vehicle.Owner = newOwner;
        await _parkingDb.SaveChangesAsync();
        // _parkingDb.Vehicles.Update(vehicle);
    }

    public async Task RemoveVehicleAsync(Vehicle vehicle)
    {
        _parkingDb.Vehicles.Remove(vehicle);
        await _parkingDb.SaveChangesAsync();
    }
}