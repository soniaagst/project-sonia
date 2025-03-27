using ParkingSystemLibrary.Models;
using ParkingSystemLibrary.Repositories;

namespace ParkingSystemAPI.Services;

public class VehicleService
{
    private IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<List<Vehicle>> GetAllVehicles()
    {
        return await _vehicleRepository.GetVehiclesAsync();
    }

    public async Task<Vehicle?> SearchByLicensePlate(string licensePlate)
    {
        return await _vehicleRepository.GetVehicleByLicensePlateAsync(licensePlate);
    }

    public async Task<List<Vehicle>> SearchByOwner(string owner)
    {
        return await _vehicleRepository.GetVehiclesByOwnerAsync(owner);
    }

    public async Task<Vehicle> RegisterVehicle(VehicleType vehicleType, string licensePlate, string owner)
    {
        Vehicle vehicle = new Vehicle{
            Type = vehicleType, 
            LicensePlate = licensePlate, 
            Owner = owner};

        await _vehicleRepository.AddVehicleAsync(vehicle);

        return vehicle;
    }

    public async Task<bool> EditVehicleOwner(string licensePlate, string newOwner)
    {
        var vehicle = await _vehicleRepository.GetVehicleByLicensePlateAsync(licensePlate);

        if (vehicle is null) return false;
        
        await _vehicleRepository.UpdateVehicleOwnerAsync(vehicle, newOwner);
        return true;
    }

    public async Task<bool> UnregVehicle(string licensePlate)
    {
        Vehicle? vehicle = await _vehicleRepository.GetVehicleByLicensePlateAsync(licensePlate);
        
        if (vehicle is not null)
        {
            await _vehicleRepository.RemoveVehicleAsync(vehicle);
            return true;
        }
        return false;
    }
}