using CarDataContract;
using System.Collections.Generic;

namespace AutoRepository
{
    public interface ICarRepository
    {
        Car Add(CarContract car);
        Car GetCarById(int carId);
        IEnumerable<Car> GetCars();
        bool Delete(int carId);
    }
}
