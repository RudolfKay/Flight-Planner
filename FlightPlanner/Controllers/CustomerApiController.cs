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
            var f = FlightStorage.SearchForFlight(flight);

            if (f == null)
            {
                return Ok();
            }

            return Ok(flight);
        }
    
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlightById(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            
            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
