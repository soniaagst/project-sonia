using Microsoft.EntityFrameworkCore;
using ParkingSystemLibrary.Models;

namespace ParkingSystemLibrary;
public class ParkingSystemAntarmuka
{
    private readonly VehicleDb _vehicleDb;
    private ParkingLot _parkingLot;

    public ParkingSystemAntarmuka(ParkingLot parkingLot, VehicleDb vehicleDb)
    {
        _vehicleDb = vehicleDb;
        _parkingLot = parkingLot;
    }

    public async Task<List<Vehicle>> GetVehiclesData()
    {
        return await _vehicleDb.Vehicles.ToListAsync();
    }

    public async Task RegisterVehicle(VehicleType vehicleType, string licensePlate, string owner)
    {
        Vehicle vehicle = new(vehicleType, licensePlate, owner);

        _vehicleDb.Vehicles.Add(vehicle);
        await _vehicleDb.SaveChangesAsync();
    }

    public async Task<List<Vehicle>> SearchbyOwner(string owner)
    {
        return await _vehicleDb.Vehicles.Where(vhcl => vhcl.Owner == owner).ToListAsync();
    }

    public async Task<Vehicle?> SearchByLicensePlate(string licensePlate)
    {
        return await _vehicleDb.Vehicles.FirstOrDefaultAsync(vhcl => vhcl.LicensePlate == licensePlate);
    }

    public async Task<bool> UnregVehicle(string licensePlate)
    {
        Vehicle? vehicle = await SearchByLicensePlate(licensePlate);
        
        if (vehicle is not null)
        {
            _vehicleDb.Vehicles.Remove(vehicle);
            await _vehicleDb.SaveChangesAsync();

            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<Karcis?> ParkVehicle(string licensePlate)
    {
        Vehicle? vehicle = await SearchByLicensePlate(licensePlate);

        if (vehicle is null) return null;

        return _parkingLot.ParkVehicle(vehicle);
    }

    public async Task<double?> UnparkVehicle(string licensePlate, Guid karcisId)
    {
        Vehicle? vehicle = await _vehicleDb.Vehicles.FirstOrDefaultAsync(vhc => vhc.LicensePlate == licensePlate);

        if (vehicle is not null)
        {
            return _parkingLot.RemoveVehicle(vehicle, karcisId);
        }

        return null;
    }
}