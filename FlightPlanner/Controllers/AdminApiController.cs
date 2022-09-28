using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private static readonly object taskLock = new();

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            lock(taskLock)
            {
                if (FlightStorage.GetFlight(id) == null)
                {
                    return NotFound(); //404
                }
            }
            
            return Ok(); //200
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            lock (taskLock)
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
            }

            return Created("",flight); //201
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock (taskLock)
            {
                var flight = FlightStorage.GetFlight(id);

                if (flight == null)
                {
                    return Ok();
                }

                FlightStorage.DeleteFlight(flight);
            }

            return Ok();
        }
    }
}
