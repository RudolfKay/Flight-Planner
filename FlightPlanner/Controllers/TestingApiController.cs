using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : ControllerBase
    {
        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            FlightStorage.Clear();

            return Ok();
        }
    }
}
