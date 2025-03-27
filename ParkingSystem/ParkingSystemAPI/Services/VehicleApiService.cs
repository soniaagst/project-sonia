using AutoMapper;
using ParkingSystemAPI.DTOs;
using ParkingSystemLibrary.Models;
using ParkingSystemLibrary.Repositories;

namespace ParkingSystemAPI.Services;

public class VehicleApiService
{
    private VehicleRepository _vehicleRepository;
    private IMapper _mapper;

    public VehicleApiService(VehicleRepository vehicleRepository, IMapper mapper)
    {
        _vehicleRepository = vehicleRepository;
        _mapper = mapper;
    }

    public async Task<List<VehicleDto>> GetAllVehicles()
    {
        List<Vehicle> vehicles = await _vehicleRepository.GetVehiclesAsync();
        List<VehicleDto> vehicleDtos = [];

        foreach(var vehicle in vehicles)
            vehicleDtos.Add(_mapper.Map<VehicleDto>(vehicle));

        return vehicleDtos;
    }

    public async Task<VehicleDto?> SearchByLicensePlate(string licensePlate)
    {
        var vehicle = await _vehicleRepository.GetVehicleByLicensePlateAsync(licensePlate);

        if (vehicle is not null)
            return _mapper.Map<VehicleDto>(vehicle);

        else return null;
    }

    public async Task<List<VehicleDto>> SearchByOwner(string owner)
    {
        List<Vehicle> vehicles = await _vehicleRepository.GetVehiclesByOwnerAsync(owner);
        List<VehicleDto> vehicleDtos = [];

        foreach (var vehicle in vehicles)
            vehicleDtos.Add(_mapper.Map<VehicleDto>(vehicle));

        return vehicleDtos;
    }

    public async Task<VehicleDto> RegisterVehicle(VehicleType vehicleType, string licensePlate, string owner)
    {
        Vehicle vehicle = new(vehicleType, licensePlate, owner);

        await _vehicleRepository.AddVehicleAsync(vehicle);

        return _mapper.Map<VehicleDto>(vehicle);
    }

    public async Task<bool> EditVehicleOwner(string licensePlate, string newOwner)
    {
        var vehicle = await _vehicleRepository.GetVehicleByLicensePlateAsync(licensePlate);

        if (vehicle is null) return false;
        await _vehicleRepository.UpdateVehicleOwnerAsync(vehicle, newOwner);
        return true;
    }

    public async Task<bool> UnregVehicle(string licensePlate)
    {
        Vehicle? vehicle = await _vehicleRepository.GetVehicleByLicensePlateAsync(licensePlate);
        
        if (vehicle is not null)
        {
            await _vehicleRepository.RemoveVehicleAsync(vehicle);
            return true;
        }
        return false;
    }
}