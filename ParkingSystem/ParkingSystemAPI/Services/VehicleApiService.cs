using AutoMapper;
using ParkingSystemAPI.DTOs;
using ParkingSystemLibrary.Models;
using ParkingSystemLibrary.Services;

namespace ParkingSystemAPI.Services;

public class VehicleApiService
{
    private VehicleService _vehicleService;
    private IMapper _mapper;

    public VehicleApiService(VehicleService vehicleService, IMapper mapper)
    {
        _vehicleService = vehicleService;
        _mapper = mapper;
    }

    public async Task<List<VehicleDto>> GetAllVehicles()
    {
        List<Vehicle> vehicles = await _vehicleService.GetVehiclesData();
        List<VehicleDto> vehicleDtos = [];

        foreach(var vehicle in vehicles)
            vehicleDtos.Add(_mapper.Map<VehicleDto>(vehicle));

        return vehicleDtos;
    }

    public async Task<VehicleDto?> SearchByLicensePlate(string licensePlate)
    {
        var vehicle = await _vehicleService.SearchByLicensePlate(licensePlate);

        if (vehicle is not null)
            return _mapper.Map<VehicleDto>(vehicle);
        else return null;
    }

    public async Task<List<VehicleDto>> SearchByOwner(string owner)
    {
        List<Vehicle> vehicles = await _vehicleService.SearchByOwner(owner);
        List<VehicleDto> vehicleDtos = [];

        foreach (var vehicle in vehicles)
            vehicleDtos.Add(_mapper.Map<VehicleDto>(vehicle));

        return vehicleDtos;
    }

    public async Task<Vehicle> RegisterVehicle(VehicleType vehicleType, string licensePlate, string owner)
    {
        return await _vehicleService.RegisterVehicle(vehicleType, licensePlate, owner);
    }

    public async Task<bool> UnregVehicle(string licensePlate)
    {
        return await _vehicleService.UnregisterVehicle(licensePlate);
    }
}