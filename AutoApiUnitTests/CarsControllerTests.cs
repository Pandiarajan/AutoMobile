using AutoApi.Controllers;
using AutoApi.Model;
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
            carsController = new CarsController();
        }

        [Fact]
        public void Post_ShouldCreateACar_AndReturn_GivenValidContract()
        {
            var contract = new CarContract { Title = "BMW Car", FirstRegistration = new DateTime(2018, 05, 05) };
            var result = carsController.Post(contract);

            Assert.True(result.Value.Id > 0);
            Assert.Same(result.Value.Title, contract.Title);
            Assert.Equal(result.Value.FirstRegistration, contract.FirstRegistration);
        }        
    }
}
