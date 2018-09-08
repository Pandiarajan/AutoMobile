using AutoApi.Config;
using AutoApi.Controllers;
using AutoMapper;
using AutoRepository;
using CarDataContract;
using System;
using System.Linq;
using Xunit;

namespace AutoApiUnitTests
{
    public class CarsControllerTests
    {
        ICarsController carsController;

        public CarsControllerTests()
        {           
            carsController = new CarsController(new CarRepository(Config.GetMapper()));
        }

        [Fact]
        public void Post_ShouldCreateACar_AndReturn_GivenValidContract()
        {
            var contract = TestHelper.GetContract();
            var result = carsController.Post(contract).GetCar();
            
            Assert.True(result.Id > 0);
            Assert.Same(contract.Title, result.Title);
            Assert.Equal(contract.FirstRegistration, result.FirstRegistration);
        }

        [Fact]
        public void Get_ShouldGive_Cars()
        {
            carsController.Post(TestHelper.GetContract());

            var getResult = carsController.Get().GetCars();
            Assert.True(getResult.Any());
        }
    }
}
