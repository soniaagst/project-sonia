using ParkingSystemLibrary.Models;
using ParkingSystemLibrary.Repositories;

namespace ParkingSystemAPI.Services;

public class ParkingLotService
{
    private IVehicleRepository _vehicleRepository;
    private ParkingLot _parkingLot;    

    public ParkingLotService(IVehicleRepository vehicleRepository, ParkingLot parkingLot)
    {
        _vehicleRepository = vehicleRepository;
        _parkingLot = parkingLot;
    }

    public Karcis? ParkVehicle(Vehicle vehicle)
    {
        return _parkingLot.ParkVehicle(vehicle);
    }

    public async Task<double?> UnparkVehicle(string licensePlate, string karcisId)
    {
        Vehicle? vehicle = await _vehicleRepository.GetVehicleByLicensePlateAsync(licensePlate);

        if (vehicle is not null)
        {
            Guid parsedKarcisId = Guid.Parse(karcisId);
            return _parkingLot.RemoveVehicle(vehicle, parsedKarcisId);
        }

        return null;
    }
}