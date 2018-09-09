using CarDataContract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoRepository
{
    public class InMemoryDataStore : IDataStore
    {
        private List<CarEntity> cars = new List<CarEntity>();
        private object _lockObject_ = new object();
        static volatile int id;

        public IEnumerable<CarEntity> Cars => cars;

        public InMemoryDataStore()
        {
            cars.Add(new CarEntity { Id = 1, Title = "BMW Fast", FirstRegistration = new DateTime(2010, 05, 05), IsNew = false, Fuel = Fuel.Diesel, Mileage = 20, Price = 10000 });
            cars.Add(new CarEntity { Id = 2, Title = "Audi Fresh Car", IsNew = true, Fuel = Fuel.Gasoline, Price = 20000 });
            cars.Add(new CarEntity { Id = 3, Title = "Bugati Mine", FirstRegistration = new DateTime(2017, 08, 28), IsNew = false, Fuel = Fuel.Gasoline, Mileage = 18, Price = 15000 });
            cars.Add(new CarEntity { Id = 4, Title = "Volkswagen Less Price", FirstRegistration = new DateTime(2016, 03, 18), IsNew = false, Fuel = Fuel.Diesel, Mileage = 18, Price = 25000 });
            cars.Add(new CarEntity { Id = 5, Title = "Mercedes Benz Fresh", IsNew = true, Fuel = Fuel.Gasoline, Price = 28000 });
            id = cars.Count;
        }
        private int GetNextNumber()
        {
            lock (_lockObject_)
            {
                return ++id;
            }
        }
        public void Add(CarEntity car)
        {            
            car.Id = GetNextNumber();
            cars.Add(car);
        }
        public CarEntity FirstOrDefault(Func<CarEntity, bool> predicate)
        {
            return cars.FirstOrDefault(predicate);
        }

        public void Update(CarEntity car)
        {
            int index = cars.IndexOf(cars.Single(c => c.Id == car.Id));
            cars.RemoveAt(index);
            cars.Insert(index, car);
        }

        public bool Exists(Predicate<CarEntity> predicate)
        {
            return cars.Exists(predicate);
        }
    }
}
