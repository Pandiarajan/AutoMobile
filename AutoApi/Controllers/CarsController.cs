using System.Collections.Generic;
using AutoApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace AutoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase, ICarsController
    {
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get()
        {
            throw new System.NotImplementedException();
        }

        [HttpGet("{id}")]
        public ActionResult<Car> Get(int id)
        {
            throw new System.NotImplementedException();
        }
        [HttpPost]
        public ActionResult<Car> Post([FromBody] CarContract car)
        {
            throw new System.NotImplementedException();
        }
        [HttpDelete("{id}")]
        public void Delete(int carId)
        {
            throw new System.NotImplementedException();
        }
    }
}
