using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDataContract;

namespace AutoRepository
{
    public class CarRepository : ICarRepository
    {       
        private readonly IMapper mapper;
        private readonly IDataStore dataStore;

        public CarRepository(IMapper mapper, IDataStore dataStore)
        {
            this.mapper = mapper;
            this.dataStore = dataStore;            
        }

        public Car Add(CarContract carContract, string createdByEmail)
        {
            var car = mapper.Map<CarEntity>(carContract);
            car.CreatedDateTime = DateTime.Now;
            car.LastUpdatedDateTime = car.CreatedDateTime;
            car.CreatedByEmail = createdByEmail;
            dataStore.Add(car);
            return mapper.Map<Car>(car);
        }

        public bool Delete(int carId, string updatedByEmail)
        {
            var car = dataStore.FirstOrDefault(c => c.Id == carId && !c.IsDeleted);            
            if (car != null && ValidateUserPermission(updatedByEmail, car.CreatedByEmail))
            {
                car.LastUpdatedDateTime = DateTime.Now;
                car.IsDeleted = true;
                return true;
            }
            return false;
        }        

        public Car GetCarById(int carId)
        {
            var car = dataStore.FirstOrDefault(c => c.Id == carId && !c.IsDeleted);
            if (car != null)
            {
                return mapper.Map<Car>(car);
            }
            return null;
        }

        public IQueryable<Car> GetCars()
        {
            return GetActiveCars().AsQueryable();
                        
        }

        private IEnumerable<Car> GetActiveCars()
        {
            foreach (var carEntity in dataStore.Cars)
            {
                if (!carEntity.IsDeleted)
                    yield return mapper.Map<Car>(carEntity);
            }
        }        

        public void Update(Car car)
        {
            var carEntity = dataStore.FirstOrDefault(c => c.Id == car.Id && !c.IsDeleted);
            car.CreatedDateTime = carEntity.CreatedDateTime;
            car.LastUpdatedDateTime = DateTime.Now;
            var newCarEntity = mapper.Map<CarEntity>(car);
            dataStore.Update(newCarEntity);
        }

        public bool Exists(int carId, string updatedByEmail)
        {
            return dataStore.Exists(c => !c.IsDeleted && c.Id == carId && ValidateUserPermission(updatedByEmail, c.CreatedByEmail));
        }

        private bool ValidateUserPermission(string updatedByEmail, string createdByEmail)
        {
            //Ideally this is done by authentication module through authorization
            return string.Compare(updatedByEmail, createdByEmail) == 0;            
        }
    }
}
