using AutoApi.Controllers;
using AutoMapper;
using AutoRepository;
using CarDataContract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace AutoApiUnitTests
{
    public class CarsControllerTests
    {
        ICarsController carsController;
        IMapper mapper;
        public CarsControllerTests()
        {
            mapper = Config.GetMapper();
            carsController = new CarsController(new CarRepository(mapper, new InMemoryDataStore()), mapper);
        }

        [Fact]
        public void Post_ShouldCreateACar_AndReturn_GivenValidContract()
        {
            var contract = TestHelper.GetUsedCarContract();
            var result = carsController.Post(contract).GetCar();
            
            Assert.True(result.Id > 0);
            Assert.Same(contract.Title, result.Title);
            Assert.Equal(contract.FirstRegistration, result.FirstRegistration);
        }

        [Fact]
        public void Put_ShouldUpdateExistingCar_GivenValidContract()
        {
            var contract = TestHelper.GetUsedCarContract();
            var car = carsController.Post(contract).GetCar();
            string newTitle = "New Title";
            int newPrice = 56789;

            car.Title = newTitle;
            car.Price = newPrice;

            var result = carsController.Put(car);
            var carAgain = carsController.Get(car.Id).GetCar();

            Assert.IsType<OkResult>(result);
            Assert.Equal(newTitle, carAgain.Title);
            Assert.Equal(newPrice, carAgain.Price);
        }

        [Fact]
        public void Put_ShouldReturnNotFound_WhenCarNotPresent_GivenValidContract()
        {
            var contract = TestHelper.GetUsedCarContract();
            var car = mapper.Map<Car>(contract);
            int invalidCarId = 10000;
            car.Id = invalidCarId;
            var result = carsController.Put(car);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_ShouldGive_Cars()
        {
            carsController.Post(TestHelper.GetUsedCarContract());

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
            carsController.Post(TestHelper.GetUsedCarContract());
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
            var car = carsController.Post(TestHelper.GetUsedCarContract()).GetCar();
            carsController.Delete(car.Id);

            var deleteResult = carsController.Delete(car.Id);

            Assert.IsType<NotFoundResult>(deleteResult);
        }

        [Fact]
        public void GetCarById_ShouldGiveCar_WhichIsPresent()
        {
            var car = carsController.Post(TestHelper.GetUsedCarContract()).GetCar();

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
