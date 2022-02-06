using System.Linq;
using FlightPlanner_Web.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner_Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirport(string search)
        {
            var airports = FlightStorage.FindAirport(search);
            return Ok(airports);
        }
    }
}
