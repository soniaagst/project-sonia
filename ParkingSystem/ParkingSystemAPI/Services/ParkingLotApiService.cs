using ParkingSystemAPI.DTOs;
using ParkingSystemLibrary.Services;
using AutoMapper;

namespace ParkingSystemAPI.Services;

public class ParkingLotApiService
{
    private ParkingLotService _parkingLotService;
    private IMapper _mapper;

    public ParkingLotApiService(ParkingLotService parkingLotService, IMapper mapper)
    {
        _parkingLotService = parkingLotService;
        _mapper = mapper;
    }

    public async Task<KarcisDto?> ParkVehicle(string licensePlate)
    {
        var karcis = await _parkingLotService.ParkVehicle(licensePlate);
        
        if (karcis is null) return null;
        return _mapper.Map<KarcisDto>(karcis);
    }

    public async Task<double?> UnparkVehicle(string licensePlate, string karcisId)
    {
        return await _parkingLotService.UnparkVehicle(licensePlate, karcisId);
    }
}