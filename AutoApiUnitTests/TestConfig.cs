using AutoApi.Config;
using AutoMapper;

namespace AutoApiUnitTests
{
    public class Config
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
                cfg.AddProfile<TestMappingProfile>();
            });
            return new Mapper(config);
        }
    }
}
