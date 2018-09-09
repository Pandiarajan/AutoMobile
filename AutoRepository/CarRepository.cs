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

        public Car Add(CarContract carContract)
        {
            var car = mapper.Map<CarEntity>(carContract);
            dataStore.Add(car);
            return mapper.Map<Car>(car);
        }

        public bool Delete(int carId)
        {
            var car = dataStore.FirstOrDefault(c => c.Id == carId && !c.IsDeleted);
            if (car != null)
            {
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
            dataStore.Update(mapper.Map<CarEntity>(car));
        }

        public bool Exists(int carId)
        {
            return dataStore.Exists(c => !c.IsDeleted && c.Id == carId);
        }
    }
}
