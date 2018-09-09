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
            var mapper = Config.GetMapper();
            carsController = new CarsController(new CarRepository(mapper, new InMemoryDataStore()), mapper);
        }

        [Fact]
        public void Post_ShouldCreateACar_AndReturn_GivenValidContract()
        {
            var contract = TestHelper.GetOldCarContract();
            var result = carsController.Post(contract).GetCar();
            
            Assert.True(result.Id > 0);
            Assert.Same(contract.Title, result.Title);
            Assert.Equal(contract.FirstRegistration, result.FirstRegistration);
        }

        [Fact]
        public void Get_ShouldGive_Cars()
        {
            carsController.Post(TestHelper.GetOldCarContract());

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
            carsController.Post(TestHelper.GetOldCarContract());
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
            var car = carsController.Post(TestHelper.GetOldCarContract()).GetCar();
            carsController.Delete(car.Id);

            var deleteResult = carsController.Delete(car.Id);

            Assert.IsType<NotFoundResult>(deleteResult);
        }

        [Fact]
        public void GetCarById_ShouldGiveCar_WhichIsPresent()
        {
            var car = carsController.Post(TestHelper.GetOldCarContract()).GetCar();

            var carResult = carsController.Get(car.Id).GetCar();

            Assert.Equal(car.Id, carResult.Id);
            Assert.Equal(car.Title, carResult.Title);
            Assert.Equal(car.FirstRegistration, carResult.FirstRegistration);

        }

        [Fact]
        public void GetCarById_ShouldReturnNotFound_WhichIsNotPresent()
        {
            var carResult = carsController.Get(10000);
            Assert.IsType<NotFoundResult>(carResult.Result);
        }
    }
}
