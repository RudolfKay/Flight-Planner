using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        private readonly FlightStorage _flightStorage;
        private static readonly object taskLock = new();

        public AdminApiController(FlightPlannerDbContext context)
        {
            _context = context;
            _flightStorage = new FlightStorage(context);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            Flight flight = null;

            lock(taskLock)
            {
                flight = _context.Flights.
                    Include(f => f.From).
                    Include(f => f.To).
                    FirstOrDefault(f => f.Id == id);

                if (flight == null)
                {
                    return NotFound(); //404
                }
            }
            
            return Ok(flight); //200
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            lock (taskLock)
            {
                if (_flightStorage.IsFlightNullOrEmpty(flight) == null || 
                    _flightStorage.IsAirportValid(flight) == null ||
                    _flightStorage.IsTimeValid(flight) == null)
                {
                    return BadRequest(); //400
                }
                if (_context.Flights.FirstOrDefault(f => f.Id == flight.Id) != null || 
                    _flightStorage.IsFlightValid(flight) == null)
                {
                    return Conflict(); //409
                }

                _context.Flights.Add(flight);
                _context.SaveChanges();
            }

            return Created("",flight); //201
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            Flight flight = null;

            lock (taskLock)
            {
                flight = _context.Flights.FirstOrDefault(f => f.Id == id);

                if (flight == null)
                {
                    return Ok();
                }

                _context.Flights.Remove(flight);
                _context.SaveChanges();
            }

            return Ok();
        }
    }
}
