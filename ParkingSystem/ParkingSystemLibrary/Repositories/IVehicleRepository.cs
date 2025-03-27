using ParkingSystemLibrary.Models;

namespace ParkingSystemLibrary.Repositories;

public interface IVehicleRepository
{
    public Task<List<Vehicle>> GetVehiclesAsync();
    public Task<List<Vehicle>> GetVehiclesByOwnerAsync(string owner);
    public Task<Vehicle?> GetVehicleByLicensePlateAsync(string licensePlate);
    public Task AddVehicleAsync(Vehicle vehicle);
    public Task UpdateVehicleOwnerAsync(Vehicle vehicle, string newOwner);
    public Task RemoveVehicleAsync(Vehicle vehicle);
}