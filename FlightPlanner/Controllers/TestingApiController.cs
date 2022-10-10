using FlightPlanner.Core.Services;
using FlightPlanner.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public TestingApiController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _flightService.ClearData();

            return Ok();
        }
    }
}
