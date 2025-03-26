using ParkingSystemLibrary.Models;

namespace ParkingSystemAPI.DTOs;

public class VehicleDto
{
    public string LicensePlate {get; set;}
    public VehicleType Type {get; set;}
    public string Owner {get; set;}
}