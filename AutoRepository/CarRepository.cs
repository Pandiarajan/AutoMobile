using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDataContract;

namespace AutoRepository
{
    public class CarRepository : ICarRepository
    {
        List<CarEntity> cars = new List<CarEntity>();
        static volatile int id = 0;
        object _lockObject_ = new object();
        private readonly IMapper mapper;

        public CarRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public Car Add(CarContract carContract)
        {
            var car = mapper.Map<CarEntity>(carContract);
            car.Id = GetNextNumber();
            cars.Add(car);
            return mapper.Map<Car>(car);
        }

        public bool Delete(int carId)
        {
            var car = cars.FirstOrDefault(c => c.Id == carId && !c.IsDeleted);
            if (car != null)
            {
                car.IsDeleted = true;
                return true;
            }
            return false;
        }

        public Car GetCarById(int carId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Car> GetCars()
        {
            foreach (var carEntity in cars)
            {
                if (!carEntity.IsDeleted)
                    yield return mapper.Map<Car>(carEntity);
            }            
        }

        private int GetNextNumber()
        {
            lock (_lockObject_)
            {
                return ++id;
            }
        }
    }
}
