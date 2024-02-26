using System;
using Air_bnb.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Air_bnb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatsController : ControllerBase
    {
        // GET: api/<FlatsController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Flat> flats= Flat.Read();
            return flats.Count == 0 ? NotFound("There are no flats") : Ok(flats);

        }

        // GET api/<FlatsController>/5
        [HttpGet("{id}")]
        public Flat Get(string id)
        {
            return Flat.Read(id);
        }

        [HttpGet("q")]
        public IActionResult GetMaxPriceInCity(string city, int price)
        {
            List<Flat> filterdFlats = Flat.GetMaxPriceInCity(city, price);
         
            return filterdFlats.Count == 0? NotFound("Sorry no such flat exists"): Ok(filterdFlats);
        }

        // POST api/<FlatsController>
        [HttpPost]
        public IActionResult Post([FromBody] Flat flat)
        {
            return flat.Insert() ? Ok(flat) : Conflict("Unable to add the flat");
        }

        // PUT api/<FlatsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FlatsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
