using Microsoft.AspNetCore.Authorization;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Models;
using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Core.Validations;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IFlightValidator _flightValidator;

        public AdminApiController(IFlightService flightService, IFlightValidator flightValidator)
        {
            _flightService = flightService;
            _flightValidator = flightValidator;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            Flight flight = _flightService.GetCompleteFlightById(id);

            if (flight == null)
            {
                return NotFound(); //404
            }
            
            return Ok(flight); //200
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            if (_flightValidator.IsFlightValid(flight))
            {
                return BadRequest(); //400
            }

            if (!_flightService.Exists(flight) ||
                _flightService.GetCompleteFlightById(flight.Id) != null)
            {
                return Conflict(); //409
            }

            _flightService.Create(flight);

            return Created("",flight); //201
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            Flight flight = _flightService.GetCompleteFlightById(id);

            if (flight != null)
            {
                _flightService.Delete(flight);
            }

            return Ok();
        }
    }
}
