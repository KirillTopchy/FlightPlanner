using FlightPlanner_Web.Models;
using FlightPlanner_Web.Storage;
using FlightPlanner_Web.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner_Web.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        private static readonly object StorageLock = new();

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlights(int id)
        { 
            var flight = FlightStorage.GetFlight(id);
            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id) 
        {
            lock (StorageLock)
            {
                FlightStorage.DeleteFlight(id);
                return Ok();
            }
        }

        [HttpPut, Authorize]
        [Route("flights")]
        public IActionResult PutFlight(AddFlightRequest request)
        {
            lock (StorageLock)
            {
                if (!FlightValidation.FlightIsValid(request) || !AirportValidation.AirportIsValid(request))
                {
                    return BadRequest();
                }

                if (FlightStorage.Exists(request))
                {
                    return Conflict();
                }

                return Created("", FlightStorage.AddFlight(request));
            }
        }
    }
}
