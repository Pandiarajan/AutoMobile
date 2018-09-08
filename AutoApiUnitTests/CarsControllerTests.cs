using AutoApi.Controllers;
using AutoRepository;
using Microsoft.AspNetCore.Mvc;
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

        [Fact]
        public void Get_ShouldGive_EmptyCarsList_WhenNoCarsPresent()
        {
            var getResult = carsController.Get().GetCars();
            foreach (var car in getResult)
            {
                carsController.Delete(car.Id);
            }

            getResult = carsController.Get().GetCars();
            Assert.True(!getResult.Any());
        }

        [Fact]
        public void Delete_ShouldMarkTheCarAdvertisement_AsDeletedWhenPresent()
        {
            carsController.Post(TestHelper.GetContract());
            var getResult = carsController.Get().GetCars();

            foreach (var car in getResult)
            {
                var deleteResult = carsController.Delete(car.Id);
                Assert.IsType<OkResult>(deleteResult);
            }

            getResult = carsController.Get().GetCars();
            Assert.True(!getResult.Any());
        }

        [Fact]
        public void Delete_ShouldReturnNotFound_WhenCarNotPresent()
        {
            int carIdNotInSystem = 10000;
            var deleteResult = carsController.Delete(carIdNotInSystem);

            Assert.IsType<NotFoundResult>(deleteResult);
        }

        [Fact]
        public void Delete_ShouldReturnNotFound_WhenCarAlreadyMarkedAsDeleted()
        {
            var car = carsController.Post(TestHelper.GetContract()).GetCar();
            carsController.Delete(car.Id);

            var deleteResult = carsController.Delete(car.Id);

            Assert.IsType<NotFoundResult>(deleteResult);
        }

    }
}
