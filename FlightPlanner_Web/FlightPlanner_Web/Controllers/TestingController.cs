using FlightPlanner_Web.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner_Web.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        [HttpPost]
        [Route("clear")]
        public IActionResult Clear(int id)
        {
            FlightStorage.ClearFlights();
            return Ok();
        }
    }
}
