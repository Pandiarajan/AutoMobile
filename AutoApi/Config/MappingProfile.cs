using AutoMapper;
using AutoRepository;
using CarDataContract;

namespace AutoApi.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {            
            CreateMap<CarContract, CarEntity>();
            CreateMap<CarEntity, Car>();
        }
    }
}
