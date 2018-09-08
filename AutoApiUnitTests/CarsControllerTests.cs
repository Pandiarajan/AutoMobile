using AutoApi.Config;
using AutoApi.Controllers;
using AutoMapper;
using AutoRepository;
using CarDataContract;
using System;
using Xunit;

namespace AutoApiUnitTests
{
    public class CarsControllerTests
    {
        ICarsController carsController;

        public CarsControllerTests()
        {           
            carsController = new CarsController(new CarRepository(TestHelper.GetMapper()));
        }

        [Fact]
        public void Post_ShouldCreateACar_AndReturn_GivenValidContract()
        {
            var contract = new CarContract { Title = "BMW Car", FirstRegistration = new DateTime(2018, 05, 05) };
            var result = carsController.Post(contract).GetCar();
            
            Assert.Equal(1, result.Id);
            Assert.Same(contract.Title, result.Title);
            Assert.Equal(contract.FirstRegistration, result.FirstRegistration);
        }
    }
}
