using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            var airport = FlightStorage.SearchForAirport(search);

            if (airport == null)
            {
                return NotFound();
            }

            return Ok(new []{airport});
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(Flight flight)
        {
            return Ok(flight);
        }
    
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlightById(int id)
        {
           return Ok();
        }
    }
}
