using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validations;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IFlightValidator _flightValidator;

        public CustomerApiController(IFlightService flightService)
        {
            _flightService = flightService;
            _flightValidator = new FlightValidator();
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            var flights = _flightService.GetAll();
            var airport = _flightService.SearchForAirport(flights, search);

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
            var flights = _flightService.GetAll();

            var pageResult = _flightService.SearchForFlight(req, flights);

            if (pageResult == null)
            {
                return BadRequest();
            }

            return Ok(pageResult);
        }
    
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlightById(int id)
        {
            Flight flight = _flightService.GetCompleteFlightById(id);

            if (flight != null)
            {
                return Ok(flight);
            }

            return BadRequest();
        }
    }
}
