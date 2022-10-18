using FlightPlanner.Core.Services;
using FlightPlanner.Core.Models;
using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Models;
using AutoMapper;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private static readonly object taskLock = new(); 
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;

        public CustomerApiController(IFlightService flightService, IAirportService airportService, IMapper mapper)
        {
            _flightService = flightService;
            _airportService = airportService;
            _mapper = mapper;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            var airport = _airportService.SearchForAirport(search);

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
            lock (taskLock)
            {
                var pageResult = _flightService.SearchForFlight(req);

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
