using FlightPlanner_Web.Models;
using FlightPlanner_Web.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner_Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            var airports = FlightStorage.FindAirport(search);
            if (airports != null)
            {
                return Ok(airports);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightRequest req)
        {
            //if (!FlightValidation.IsValidSearchFlightRequest(request) || request.To.Equals(request.From))
            //{
            //    return BadRequest();
            //}

            return Ok(new PageResult());
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchFlights(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }
    }
}
