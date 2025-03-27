using ParkingSystemLibrary.Models;
using ParkingSystemLibrary.Repositories;

namespace ParkingSystemLibrary.Services;
public class VehicleService
{
    private readonly VehicleRepository _vehicleRepository;

    public VehicleService(VehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<List<Vehicle>> GetVehiclesData()
    {
        return await _vehicleRepository.GetVehiclesAsync();
    }

    public async Task<List<Vehicle>> SearchByOwner(string owner)
    {
        return await _vehicleRepository.GetVehiclesByOwnerAsync(owner);
    }

    public async Task<Vehicle?> SearchByLicensePlate(string licensePlate)
    {
        return await _vehicleRepository.GetVehicleByLicensePlateAsync(licensePlate);
    }

    public async Task<Vehicle> RegisterVehicle(VehicleType vehicleType, string licensePlate, string owner)
    {
        Vehicle vehicle = new(vehicleType, licensePlate, owner);

        await _vehicleRepository.AddVehicleAsync(vehicle);

        return vehicle;
    }

    public async Task<bool> UpdateVehicleOwner(string licensePlate, string newOwner)
    {
        var vehicle = await SearchByLicensePlate(licensePlate);

        if (vehicle is null) return false;
        
        await _vehicleRepository.UpdateVehicleOwnerAsync(vehicle, newOwner);
        return true;
    }

    public async Task<bool> UnregisterVehicle(string licensePlate)
    {
        Vehicle? vehicle = await SearchByLicensePlate(licensePlate);
        
        if (vehicle is not null)
        {
            await _vehicleRepository.RemoveVehicleAsync(vehicle);
            return true;
        }
        else
        {
            return false;
        }
    }
}