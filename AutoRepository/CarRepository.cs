using System.Collections.Generic;
using AutoMapper;
using CarDataContract;

namespace AutoRepository
{
    public class CarRepository : ICarRepository
    {
        List<Car> cars = new List<Car>();
        static volatile int id = 0;
        object _lockObject_ = new object();
        public Car Add(CarContract carContract)
        {
            var car = Mapper.Map<Car>(carContract);
            car.Id = GetNextNumber();
            cars.Add(car);
            return car;
        }

        public bool Delete(int carId)
        {
            throw new System.NotImplementedException();
        }

        public Car GetCarById(int carId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Car> GetCars()
        {
            throw new System.NotImplementedException();
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
