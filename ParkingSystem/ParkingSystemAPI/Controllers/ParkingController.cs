using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingSystemAPI.DTOs;
using ParkingSystemAPI.Services;
using ParkingSystemLibrary.Models;

namespace ParkingSystemAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/parking")]
public class ParkingController : ControllerBase
{
    private ParkingLotService _parkingLotService;
    private VehicleService _vehicleService;
    private IMapper _mapper;

    public ParkingController(ParkingLotService parkingLotService, VehicleService vehicleService, IMapper mapper)
    {
        _parkingLotService = parkingLotService;
        _vehicleService = vehicleService;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("/park")]
    public async Task<IActionResult> ParkVehicle(string licensePlate) {
        Vehicle? vehicle = await _vehicleService.SearchByLicensePlate(licensePlate);

        if (vehicle is null) return NotFound("Vehicle not registered.");

        var karcis = _parkingLotService.ParkVehicle(vehicle);

        if (karcis is null) return NotFound($"No free space for {vehicle.Type}.");

        return Ok(_mapper.Map<KarcisDto>(karcis));
    }
    
    [HttpPost]
    [Route("/unpark")]
    public async Task<IActionResult> UnparkVehicle(string licensePlate, string karcisId) {
        var fee = await _parkingLotService.UnparkVehicle(licensePlate, karcisId);

        if (fee is null) return BadRequest("Invalid ticket or vehicle not found.");

        return Ok($"Fee: {fee}");
    }
}