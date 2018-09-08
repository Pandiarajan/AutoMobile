using AutoMapper;
using CarDataContract;

namespace AutoApi.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarContract, Car>();
        }
    }
}
