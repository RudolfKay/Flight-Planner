using FlightPlanner.Core.Services;
using FlightPlanner.Core.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public CustomerApiController(IFlightService flightService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            var flights = _flightService.GetAll();

            if (flights.Count <= 0)
            {
                return NotFound();
            }

            var airport = _flightService.SearchForAirport(flights, search, _mapper);

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

            if (flights.Count <= 0)
            {
                return NotFound();
            }
            var pageResult = _flightService.SearchForFlight(req, flights, _mapper);

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
