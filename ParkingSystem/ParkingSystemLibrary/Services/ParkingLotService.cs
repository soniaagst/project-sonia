using ParkingSystemLibrary.Models;

namespace ParkingSystemLibrary.Services;

public class ParkingLotService
{
    private readonly VehicleService _vehicleService;
    private readonly ParkingLot _parkingLot;

    public ParkingLotService(VehicleService vehicleService, ParkingLot parkingLot)
    {
        _vehicleService = vehicleService;
        _parkingLot = parkingLot;
    }

    public async Task<Karcis?> ParkVehicle(string licensePlate)
    {
        Vehicle? vehicle = await _vehicleService.SearchByLicensePlate(licensePlate);

        if (vehicle is null) return null;

        return _parkingLot.ParkVehicle(vehicle);
    }

    public async Task<double?> UnparkVehicle(string licensePlate, string karcisId)
    {
        Vehicle? vehicle = await _vehicleService.SearchByLicensePlate(licensePlate);

        if (vehicle is not null)
        {
            Guid parsedKarcisId = Guid.Parse(karcisId);
            return _parkingLot.RemoveVehicle(vehicle, parsedKarcisId);
        }

        return null;
    }
}