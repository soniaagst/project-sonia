using Microsoft.AspNetCore.Mvc;
using ParkingSystemLibrary;
using ParkingSystemLibrary.Models;

namespace ParkingSystemAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ParkingSystemApiController : ControllerBase
{
    private ParkingSystemAntarmuka _antarmukaParkingSys;
    private VehicleDb _vehicleDb;

    public ParkingSystemApiController(ParkingSystemAntarmuka antarmuka, VehicleDb vehicleDb) {
        _antarmukaParkingSys = antarmuka;
        _vehicleDb = vehicleDb;
    }

    [HttpPost]
    [Route("/registervehicle")]
    public async Task RegisterVehicle(VehicleType vehicleType, string licensePlate, string owner) {
        await _antarmukaParkingSys.RegisterVehicle(vehicleType, licensePlate, owner);
    }

    [HttpPost]
    [Route("/park")]
    public async Task<Karcis?> ParkVehicle(string licensePlate) {
        return await _antarmukaParkingSys.ParkVehicle(licensePlate);
    }
    
    [HttpPost]
    [Route("/unpark")]
    public async Task<double?> UnparkVehicle(string licensePlate, string karcisId) {
        return await _antarmukaParkingSys.UnparkVehicle(licensePlate, karcisId);
    }

    [HttpGet]
    [Route("/searchbyowner")]
    public async Task<List<Vehicle>> SearchByOwner(string owner) {
        return await _antarmukaParkingSys.SearchbyOwner(owner);
    }

    [HttpGet]
    [Route("/searchbylicense")]
    public async Task<Vehicle?> SearchByLicense(string licensePlate) {
        return await _antarmukaParkingSys.SearchByLicensePlate(licensePlate);
    }

    [HttpGet]
    [Route("/getallvehicle")]
    public async Task<List<Vehicle>> GetVehicles() {
        return await _antarmukaParkingSys.GetVehiclesData();
    }

    [HttpDelete]
    [Route("/unregistervehicle")]
    public async Task UnregVehicle(string licensePlate) {
        await _antarmukaParkingSys.UnregVehicle(licensePlate);
    }
}
