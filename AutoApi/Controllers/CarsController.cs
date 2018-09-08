using System.Collections.Generic;
using AutoRepository;
using CarDataContract;
using Microsoft.AspNetCore.Mvc;

namespace AutoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase, ICarsController
    {
        private readonly ICarRepository carRepository;

        public CarsController(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get()
        {
            return Ok(carRepository.GetCars());
        }

        [HttpGet("{id}")]
        public ActionResult<Car> Get(int id)
        {
            var car = carRepository.GetCarById(id);
            if (car != null)
                return Ok(car);
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult<Car> Post([FromBody] CarContract car)
        {
            return Ok(carRepository.Add(car));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (carRepository.Delete(id))
                return Ok();
            else
                return NotFound();
        }
    }
}
