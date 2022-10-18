using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        private static readonly object taskLock = new();
        private readonly FlightStorage _flightStorage;

        public CustomerApiController(FlightPlannerDbContext context)
        {
            _context = context;
            _flightStorage = new FlightStorage();
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            var flights = _context.Flights.
                Include(f => f.From).
                Include(f => f.To).
                ToList();

            var airport = _flightStorage.SearchForAirport(flights, search);

            if (airport == null)
            {
                return NotFound();
            }

            return Ok(new []{airport});
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(SearchFlightsRequest req)
        {
            var flights = _context.Flights.
                Include(f => f.From).
                Include(f => f.To).
                ToList();

            lock (taskLock)
            {
                var pageResult = _flightStorage.SearchForFlight(req, flights);

                if (pageResult == null)
                {
                    return BadRequest();
                }

                return Ok(pageResult);
            }
        }
    
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlightById(int id)
        {
            var flight = _flightStorage.GetFlight(id, _context);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
