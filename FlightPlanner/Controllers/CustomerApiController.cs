using FlightPlanner.Core.Services;
using FlightPlanner.Core.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FlightPlanner.Models;

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
            var airport = _flightService.SearchForAirport(search);

            if (airport == null)
            {
                return NotFound();
            }

            var request = _mapper.Map<AirportRequest>(airport);

            return Ok(new []{request});
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(SearchFlightsRequest req)
        {
            var pageResult = _flightService.SearchForFlight(req);

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

            if (flight == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<FlightRequest>(flight);

            return Ok(response);
        }
    }
}
