using System.Linq;
using AutoMapper;
using AutoRepository;
using CarDataContract;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace AutoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase, ICarsController
    {
        private readonly ICarRepository carRepository;
        private readonly IMapper mapper;

        public CarsController(ICarRepository carRepository, IMapper mapper)
        {
            this.carRepository = carRepository;
            this.mapper = mapper;
        }

        [HttpGet, EnableQuery]
        public ActionResult<IQueryable<Car>> Get()
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

        [HttpPut("{id}")]
        public ActionResult Put(int id, Car car)
        {
            if (carRepository.Exists(id))
            {                                
                carRepository.Update(car);
                return Ok();
            }
            else
                return NotFound();
        }
    }
}
