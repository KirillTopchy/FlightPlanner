using System.Linq;
using FlightPlanner_Web.Models;
using FlightPlanner_Web.Storage;
using FlightPlanner_Web.Validation;
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
        public IActionResult SearchAirports(string search)
        {
            var airports = FlightStorage.FindAirport(search);
            return Ok(airports);
        }

        [HttpPost]
        [Route("/flights/search")]
        public IActionResult SearchFlights(SearchFlightRequest search)
        {
            if (!FlightValidation.IsValidSearchFlightRequest(search) || search.To.Equals(search.From))
            {
                return BadRequest();
            }

            return Ok(SearchFlightRequest.FindFlights(search));
        }
    }
}
