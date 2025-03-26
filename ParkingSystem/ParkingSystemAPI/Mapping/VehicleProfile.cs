using AutoMapper;
using ParkingSystemAPI.DTOs;
using ParkingSystemLibrary.Models;

namespace ParkingSystemAPI.Mapping;

public class VehicleProfile : Profile
{
    public VehicleProfile()
    {
        CreateMap<Vehicle, VehicleDto>();
        CreateMap<VehicleDto, Vehicle>();
    }
}