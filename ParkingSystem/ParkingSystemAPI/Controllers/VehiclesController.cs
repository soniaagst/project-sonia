using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingSystemAPI.Services;
using ParkingSystemLibrary.Models;

namespace ParkingSystemAPI.Controllers;

[Authorize]
[ApiController]
[Route("[api/vehicles]")]
public class VehiclesController : ControllerBase
{
    private VehicleApiService _vehicleApiService;

    public VehiclesController(VehicleApiService vehicleApiService) {
        _vehicleApiService = vehicleApiService;
    }

    [HttpPost]
    [Route("/registervehicle")]
    public async Task<IActionResult> RegisterVehicle(VehicleType vehicleType, string licensePlate, string owner) {
        await _vehicleApiService.RegisterVehicle(vehicleType, licensePlate, owner);
        return Ok();
    }

    [HttpGet]
    [Route("/searchbyowner")]
    public async Task<IActionResult> SearchByOwner(string owner) {
        var vehicles = await _vehicleApiService.SearchByOwner(owner);
        
        if (vehicles.Count == 0) return NotFound();
        return Ok(vehicles);
    }

    [HttpGet]
    [Route("/searchbylicense")]
    public async Task<IActionResult> SearchByLicensePlate(string licensePlate) {
        var vehicle = await _vehicleApiService.SearchByLicensePlate(licensePlate);

        if (vehicle is null) return NotFound();
        return Ok(vehicle);
    }

    [HttpGet]
    [Route("/getallvehicle")]
    public async Task<IActionResult> GetAllVehicles() {
        var vehicles = await _vehicleApiService.GetAllVehicles();
        return Ok(vehicles);
    }

    [HttpDelete]
    [Route("/unregistervehicle")]
    public async Task<IActionResult> UnregVehicle(string licensePlate) {
        var result = await _vehicleApiService.UnregVehicle(licensePlate);
        if (result is false) return NotFound();
        return Ok();
    }
}
