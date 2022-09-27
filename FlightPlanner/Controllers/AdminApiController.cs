using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);

            if (flight == null)
            {
                return NotFound(); //404
            }

            return Ok(flight); //200
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            if (FlightStorage.IsFlightNullOrEmpty(flight) == null || 
                FlightStorage.IsAirportValid(flight) == null ||
                FlightStorage.IsTimeValid(flight) == null)
            {
                return BadRequest(); //400
            }
            if (FlightStorage.GetFlight(flight.Id) != null || 
                FlightStorage.IsFlightValid(flight) == null)
            {
                return Conflict(); //409
            }

            flight = FlightStorage.AddFlight(flight);

            return Created("",flight); //201
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);

            if (flight == null)
            {
                return Ok();
            }

            FlightStorage.DeleteFlight(flight);
            return Ok();
        }
    }
}
