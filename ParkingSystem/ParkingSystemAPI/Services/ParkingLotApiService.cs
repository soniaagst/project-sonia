using ParkingSystemAPI.DTOs;
using AutoMapper;
using ParkingSystemLibrary.Models;
using ParkingSystemLibrary.Repositories;

namespace ParkingSystemAPI.Services;

public class ParkingLotApiService
{
    private VehicleRepository _vehicleRepository;
    private VehicleApiService _vehicleService;
    private ParkingLot _parkingLot;
    private IMapper _mapper;

    public ParkingLotApiService(VehicleRepository vehicleRepository, VehicleApiService vehicleApiService, ParkingLot parkingLot, IMapper mapper)
    {
        _vehicleRepository = vehicleRepository;
        _vehicleService = vehicleApiService;
        _parkingLot = parkingLot;
        _mapper = mapper;
    }

    public async Task<KarcisDto?> ParkVehicle(string licensePlate)
    {
        Vehicle? vehicle = await _vehicleRepository.GetVehicleByLicensePlateAsync(licensePlate);

        if (vehicle is null) return null;

        var karcis = _parkingLot.ParkVehicle(vehicle);

        if (karcis is null) return null;

        return _mapper.Map<KarcisDto>(karcis);
    }

    public async Task<double?> UnparkVehicle(string licensePlate, string karcisId)
    {
        Vehicle? vehicle = await _vehicleRepository.GetVehicleByLicensePlateAsync(licensePlate);

        if (vehicle is not null)
        {
            Guid parsedKarcisId = Guid.Parse(karcisId);
            return _parkingLot.RemoveVehicle(vehicle, parsedKarcisId);
        }

        return null;
    }
}