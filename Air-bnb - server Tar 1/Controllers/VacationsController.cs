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
            return vacations.Count == 0 ? NotFound("There are no vacations") : Ok(vacations);
        }

        // GET api/<VacationsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
<<<<<<< Updated upstream
            List<Vacation> findVacations = Vacation.ReadByUserId(id);
            return findVacations != null ? Ok(findVacations) : NotFound("There is no vaction witth this ID");
=======
            Vacation? find = Vacation.Read(id);
            return find != null ? Ok(find) : NotFound("There is no vaction with this ID");
>>>>>>> Stashed changes
        }

        [HttpGet("userId/{id}")]
        public IActionResult GetByUserID(string id)
        {
            List<Vacation> find = Vacation.ReadByUserId(id);
            return find.Count != 0 ? Ok(find) : NotFound("There is no vaction for this user");
        }


        [HttpGet("getByDates/{startDate}/{endDate}")]
        public IActionResult GetByDates(DateTime startDate, DateTime endDate)
        {
            List<Vacation> vacations = Vacation.GetByDates(startDate, endDate);
            return vacations.Count == 0 ? NotFound("There are no vacations between dates") : Ok(vacations);
        }

        // POST api/<VacationsController>
        [HttpPost]
        public IActionResult Post([FromBody] Vacation vacation)
        {
<<<<<<< Updated upstream
            return vacation.Insert() ? Ok(vacation) : Conflict("Unable to add the vacation");
=======
            return vacation.Insert() ? Ok(new { id=vacation.Id }) : Conflict("Unable to add the vacation");
>>>>>>> Stashed changes
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
