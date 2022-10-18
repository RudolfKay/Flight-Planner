using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;

        public TestingApiController(IFlightService flightService, IAirportService airportService)
        {
            _flightService = flightService;
            _airportService = airportService;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _flightService.ClearFlights();
            _airportService.ClearAirports();

            return Ok();
        }
    }
}
