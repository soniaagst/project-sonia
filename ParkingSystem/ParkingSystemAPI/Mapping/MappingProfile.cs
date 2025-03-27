using AutoMapper;
using ParkingSystemAPI.DTOs;
using ParkingSystemLibrary.Models;

namespace ParkingSystemAPI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Vehicle, VehicleDto>();

        CreateMap<Karcis, KarcisDto>();

        // CreateMap<User, UserDto>();
    }
}