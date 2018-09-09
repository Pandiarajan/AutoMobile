using CarDataContract;
using System.Linq;

namespace AutoRepository
{
    public interface ICarRepository
    {
        Car Add(CarContract carContract, string createdByEmail);
        Car GetCarById(int carId);
        IQueryable<Car> GetCars();
        bool Delete(int carId, string updatedByEmail);
        void Update(Car car);
        bool Exists(int carId, string updatedByEmail);
    }
}
