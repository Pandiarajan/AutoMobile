using System;
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
            throw new System.NotImplementedException();
        }
        [HttpPost]
        public ActionResult<Car> Post([FromBody] CarContract car)
        {
            return Ok(carRepository.Add(car));
        }
        [HttpDelete("{id}")]
        public void Delete(int carId)
        {
            throw new System.NotImplementedException();
        }
    }
}
