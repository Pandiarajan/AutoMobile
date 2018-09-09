using CarDataContract;
using System.Linq;

namespace AutoRepository
{
    public interface ICarRepository
    {
        Car Add(CarContract car);
        Car GetCarById(int carId);
        IQueryable<Car> GetCars();
        bool Delete(int carId);
    }
}
