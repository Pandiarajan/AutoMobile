using CarDataContract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AutoApi.Controllers
{
    public interface ICarsController
    {
        ActionResult<IQueryable<Car>> Get();
        ActionResult<Car> Get(int id);
        ActionResult<Car> Post(CarContract car);
        ActionResult Delete(int carId);
    }
}
