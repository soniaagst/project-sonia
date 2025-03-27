using ParkingSystemLibrary.Models;

namespace ParkingSystemAPI.DTOs.Requests;

public class RegisterVehicleRequest
{
    public string LicensePlate { get; set; }
    public VehicleType Type { get; set; }
    public string Owner { get; set; }
}
