using ParkingSystemLibrary.Models;

namespace ParkingSystemAPI.DTOs;

public class KarcisDto
{
    public Guid Id {get;}
    public VehicleType VehicleType {get;}
    public DateTime EnterTime {get;}

    public KarcisDto(Karcis karcis)
    {
        Id = karcis.Id;
        VehicleType = karcis.Vehicle.Type;
        EnterTime = karcis.EnterTime;
    }
}