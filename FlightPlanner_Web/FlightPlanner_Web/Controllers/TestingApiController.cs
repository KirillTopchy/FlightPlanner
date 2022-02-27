using FlightPlanner_Web.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner_Web.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        private static readonly object FlightLock = new();

        public TestingApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            lock (FlightLock)
            {
                FlightStorage.ClearFlights(_context);

                return Ok();
            }
        }
    }
}
