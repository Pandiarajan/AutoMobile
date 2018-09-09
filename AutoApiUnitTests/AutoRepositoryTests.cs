using AutoMapper;
using AutoRepository;
using System;
using System.Linq;
using Xunit;

namespace AutoApiUnitTests
{
    public class AutoRepositoryTests
    {
        IMapper mapper;
        ICarRepository carRepository;
        IDataStore dataStore;
        public AutoRepositoryTests()
        {
            mapper = Config.GetMapper();
            dataStore = new InMemoryDataStore();
            carRepository = new CarRepository(mapper, dataStore);
        }

        [Fact]
        public void CreatedDateTime_LastUpdatedDateTime_CreatedByEmail_MustBeUpdated_WhenAnewCarIsAdded()
        {
            string email = "test@gmail.com";

            var car = carRepository.Add(TestHelper.GetUsedCarContract(), email);

            Assert.Equal(email, car.CreatedByEmail);
            Assert.True(car.CreatedDateTime > DateTime.MinValue && car.CreatedDateTime < DateTime.Now);
            Assert.Equal(car.CreatedDateTime, car.LastUpdatedDateTime);
        }

        [Fact]
        public void Delete_ShouldMarkAsDeleted_AndUpdatesLastModifiedDateTime()
        {
            string email = "test@gmail.com";
            var car = carRepository.Add(TestHelper.GetUsedCarContract(), email);  
            
            var result = carRepository.Delete(car.Id, email);
            
            Assert.True(result);
            var deletedCar = dataStore.Cars.Single(c => c.Id == car.Id);
            Assert.True(deletedCar.IsDeleted);
            Assert.True(deletedCar.LastUpdatedDateTime > deletedCar.CreatedDateTime && deletedCar.LastUpdatedDateTime < DateTime.Now);
        }

        [Fact]
        public void Update_UpdatesCar_AndUpdatesLastModifiedDateTime()
        {
            var car = carRepository.Add(TestHelper.GetUsedCarContract(), "test@gmail.com");
            int price = 67890;
            car.Price = price;

            carRepository.Update(car);

            var updatedCar = dataStore.Cars.Single(c => c.Id == car.Id);
            Assert.Equal(price, updatedCar.Price);
            Assert.True(updatedCar.LastUpdatedDateTime > updatedCar.CreatedDateTime && updatedCar.LastUpdatedDateTime < DateTime.Now);
        }

        [Fact]
        public void GetCars_DoesNot_Include_Deleted()
        {
            string email = "test@gmail.com";
            var car = carRepository.Add(TestHelper.GetUsedCarContract(), email);
            var totalCars = carRepository.GetCars().Count();

            carRepository.Delete(car.Id, email);

            var totalCarsAfterDelete = carRepository.GetCars().Count();
            Assert.Equal(totalCars-1, totalCarsAfterDelete);
        }

        [Fact]
        public void GetCarById_DoesNot_Include_Deleted()
        {
            string email = "test@gmail.com";
            var car = carRepository.Add(TestHelper.GetUsedCarContract(), email);
            var totalCars = carRepository.GetCars().Count();
            carRepository.Delete(car.Id, email);

            var deletedCar = carRepository.GetCarById(car.Id);

            Assert.Null(deletedCar);
        }
    }
}
