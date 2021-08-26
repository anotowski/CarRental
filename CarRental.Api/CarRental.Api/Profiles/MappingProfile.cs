using AutoMapper;
using CarRental.Api.Models;
using CarRental.Api.Models.Responses;
using CarRental.Application.Dto.Models;

namespace CarRental.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarInformationAsyncDto, CarInformationResponse>();
            CreateMap<ReturnCarResponseDto, ReturnCarResponse>();
        }
    }
}
