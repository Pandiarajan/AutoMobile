using CarDataContract;
using System;
using System.Collections.Generic;

namespace AutoRepository
{
    public static class InMemoryDataStore
    {
        public static IEnumerable<CarEntity> Get()
        {
            yield return new CarEntity { Id = 1, Title = "BMW 1", FirstRegistration = new DateTime(2010, 05, 05), IsNew = false, Fuel = Fuel.Diesel, Mileage = 20, Price = 10000 };
            yield return new CarEntity { Id = 2, Title = "BMW 2", IsNew = true, Fuel = Fuel.Gasoline, Price = 20000 };
            yield return new CarEntity { Id = 3, Title = "BMW 3", FirstRegistration = new DateTime(2017, 08, 28), IsNew = false, Fuel = Fuel.Gasoline, Mileage = 18, Price = 15000 };
        }
    }
}
