using AutoMapper;
using ParkingSystemAPI.DTOs;
using ParkingSystemLibrary.Models;

namespace ParkingSystemAPI.Mapping;

public class KarcisProfile : Profile
{
    public KarcisProfile()
    {
        CreateMap<Karcis, KarcisDto>();
        CreateMap<KarcisDto, Karcis>();
    }
}