using FlightPlanner_Web.Handlers;
using FlightPlanner_Web.Models;
using FlightPlanner_Web.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner_Web.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlights(int id)
        {
            if (!BasicAuthenticationHandler.isauthorized)
            {
                return Unauthorized();
            }
            return NotFound();
        }

        [HttpPut, Authorize]
        [Route("flights")]
        public IActionResult PutFlight(AddFlightRequest request)
        {
           var flight = FlightStorage.AddFlight(request);
           if (FlightStorage.flightExist)
           {
               return NotFound();
           }

           return Created("",flight);
        }
    }
}
