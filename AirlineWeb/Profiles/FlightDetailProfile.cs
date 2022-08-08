using AutoMapper;
using AirlineWeb.Dtos;
using AirlineWeb.Models;

namespace AirlineWeb.Profiles
{
    public class FlightDetailProfile : Profile
    {
        public FlightDetailProfile()
        {
        // source -> target
        CreateMap<FlightDetail, FlightDetailReadDto>();
        CreateMap<FlightDetailCreateDto, FlightDetail>();
        CreateMap<FlightDetailUpdateDto, FlightDetail>();
        }
    }
}