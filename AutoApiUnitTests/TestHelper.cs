using AutoApi.Config;
using AutoMapper;
using CarDataContract;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AutoApiUnitTests
{
    public static class TestHelper
    {
        public static Car GetCar(this ActionResult<Car> actionResultCar)
        {
            var okResult = Assert.IsType<OkObjectResult>(actionResultCar.Result);
            return (Car)okResult.Value;
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            return new Mapper(config);
        }
    }
}
