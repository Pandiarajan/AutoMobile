using CarDataContract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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

        public static IQueryable<Car> GetCars(this ActionResult<IQueryable<Car>> actionResultCar)
        {
            var okResult = Assert.IsType<OkObjectResult>(actionResultCar.Result);
            return (IQueryable<Car>)okResult.Value;
        }

        public static CarContract GetUsedCarContract()
        {
            Random r = new Random();
            return new CarContract { IsNew = false, Title = "BMW Car" + r.Next(1,1000), FirstRegistration = new DateTime(2018, r.Next(1,12) , r.Next(1,28)) };
        }
    }
}
