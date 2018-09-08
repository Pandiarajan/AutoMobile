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
    }
}
