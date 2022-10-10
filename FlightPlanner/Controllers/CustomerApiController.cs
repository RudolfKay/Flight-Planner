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
        private readonly IFlightService<Flight> _flightService;
        private readonly IFlightValidator _flightValidator;

        public CustomerApiController(IFlightService<Flight> flightService)
        {
            _flightService = flightService;
            _flightValidator = new FlightValidator();
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            /*var flights = _context.Flights.
                Include(f => f.From).
                Include(f => f.To).
                ToList();*/

            return Ok();
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(SearchFlightsRequest req)
        {
            /*var flights = _context.Flights.
                Include(f => f.From).
                Include(f => f.To).
                ToList();*/

            return Ok();
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
