using AutoMapper;
using AutoRepository;
using CarDataContract;

namespace AutoApiUnitTests
{
    public class TestMappingProfile : Profile
    {
        public TestMappingProfile()
        {
            CreateMap<CarContract, Car>();
        }
    }
}
