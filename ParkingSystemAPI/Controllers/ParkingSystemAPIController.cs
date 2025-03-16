using Microsoft.AspNetCore.Mvc;
using ParkingSystem;

namespace ParkingSystemAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ParkingSystemApiController : ControllerBase
{
    private AntarmukaParkingSys antarmukaParkingSys;

    public ParkingSystemApiController(AntarmukaParkingSys antarmuka) {
        antarmukaParkingSys = antarmuka;
    }

    [HttpPost]
    [Route("/registervehicle")]
    public void RegisterVehicle(VehicleType vehicleType, string licensePlate, string owner) {
        antarmukaParkingSys.RegisterVehicle(vehicleType, licensePlate, owner);
    }

    [HttpPost]
    [Route("/park")]
    public Karcis? ParkVehicle(string licensePlate) {
        return antarmukaParkingSys.ParkVehicle(licensePlate);
    }
    
    [HttpPost]
    [Route("/unpark")]
    public double? UnparkVehicle(string licensePlate, string karcisId) {
        return antarmukaParkingSys.UnparkVehicle(licensePlate, karcisId);
    }

    [HttpGet]
    [Route("/searchbyowner")]
    public List<Vehicle> SearchByOwner(string owner) {
        return antarmukaParkingSys.SearchbyOwner(owner);
    }

    [HttpGet]
    [Route("/searchbylicense")]
    public Vehicle? SearchByLicense(string licensePlate) {
        return antarmukaParkingSys.SearchByLicensePlate(licensePlate);
    }

    [HttpGet]
    [Route("/getallvehicle")]
    public List<Vehicle> GetVehicles() {
        return antarmukaParkingSys.GetVehiclesData();
    }

    [HttpDelete]
    [Route("/unregistervehicle")]
    public void UnregVehicle(string licensePlate) {
        antarmukaParkingSys.UnregVehicle(licensePlate);
    }
}
