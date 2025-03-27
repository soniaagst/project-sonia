using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingSystemAPI.Services;

namespace ParkingSystemAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/parking")]
public class ParkingController : ControllerBase
{
    private ParkingLotService _parkingLotService;

    public ParkingController(ParkingLotService parkingLotApiService)
    {
        _parkingLotService = parkingLotApiService;
    }

    [HttpPost]
    [Route("/park")]
    public async Task<IActionResult> ParkVehicle(string licensePlate) {
        var karcis = await _parkingLotService.ParkVehicle(licensePlate);
        if (karcis is null) return BadRequest();
        return Ok(karcis);
    }
    
    [HttpPost]
    [Route("/unpark")]
    public async Task<IActionResult> UnparkVehicle(string licensePlate, string karcisId) {
        var fee = await _parkingLotService.UnparkVehicle(licensePlate, karcisId);
        if (fee is null) return BadRequest();
        return Ok(fee);
    }
}