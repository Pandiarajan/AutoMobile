using CarDataContract;
using System;
using System.Collections.Generic;

namespace AutoRepository
{
    public class InMemoryDataStore : IDataStore
    {
        public IEnumerable<CarEntity> Get()
        {            
            yield return new CarEntity { Id = 1, Title = "BMW Fast", FirstRegistration = new DateTime(2010, 05, 05), IsNew = false, Fuel = Fuel.Diesel, Mileage = 20, Price = 10000 };
            yield return new CarEntity { Id = 2, Title = "Audi Fresh Car", IsNew = true, Fuel = Fuel.Gasoline, Price = 20000 };
            yield return new CarEntity { Id = 3, Title = "Bugati Mine", FirstRegistration = new DateTime(2017, 08, 28), IsNew = false, Fuel = Fuel.Gasoline, Mileage = 18, Price = 15000 };
            yield return new CarEntity { Id = 4, Title = "Volkswagen Less Price", FirstRegistration = new DateTime(2016, 03, 18), IsNew = false, Fuel = Fuel.Diesel, Mileage = 18, Price = 25000 };
            yield return new CarEntity { Id = 5, Title = "Mercedes Benz Fresh", IsNew = true, Fuel = Fuel.Gasoline, Price = 28000 };
        }
    }
}
