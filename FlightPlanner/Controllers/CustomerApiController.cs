using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        private readonly FlightStorage _flightStorage;
        private static readonly object taskLock = new object();

        public CustomerApiController(FlightPlannerDbContext context)
        {
            _context = context;
            _flightStorage = new FlightStorage(context);
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            var airport = _flightStorage.SearchForAirport(search);

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
            lock (taskLock)
            {
                var pageResult = _flightStorage.SearchForFlight(req);

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
            var flight = FlightStorage.GetFlight(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
