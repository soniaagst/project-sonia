using ParkingSystemLibrary.Models;

namespace ParkingSystemAPI.DTOs;

public class VehicleDto
{
    public string LicensePlate {get;}
    public VehicleType VehicleType {get;}

    public VehicleDto(Vehicle vehicle)
    {
        LicensePlate = vehicle.LicensePlate;
        VehicleType = vehicle.Type;
    }
}