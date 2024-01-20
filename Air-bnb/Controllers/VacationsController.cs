using Air_bnb.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Air_bnb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationsController : ControllerBase
    {
        // GET: api/<VacationsController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Vacation> vacations = Vacation.Read();
            return vacations.Count ==0 ?NotFound("There are no vacations"): Ok(vacations);
        }

        [HttpGet("getByDates/{startDate}/{endDate}")]
        public IActionResult GetByDates(DateTime startDate, DateTime endDate)
        {
            List<Vacation> vacations = Vacation.GetByDates(startDate,endDate);
            return vacations.Count == 0 ? NotFound("There are no vacations between dates") : Ok(vacations);
        }

        // GET api/<VacationsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VacationsController>
        [HttpPost]
        public IActionResult Post([FromBody] Vacation vacation)
        {
            return vacation.Insert() ? Ok(vacation.Id) : Conflict("Unable to add the vacation");
        }

        // PUT api/<VacationsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VacationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
