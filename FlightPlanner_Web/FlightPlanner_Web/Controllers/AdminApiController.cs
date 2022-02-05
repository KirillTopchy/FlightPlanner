using System.Collections.Generic;
using FlightPlanner_Web.Handlers;
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
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlights(int id)
        {
            return NotFound();
        }

        [HttpPut, Authorize]
        [Route("flights")]
        public IActionResult PutFlight(AddFlightRequest request)
        {
            if (!FlightValidation.FlightIsValid(request))
            {
                return BadRequest();
            }

            if (!AirportValidation.AirportIsValid(request))
            {
                return BadRequest();
            }

            if (FlightStorage.Exists(request))
            {
                return Conflict();
            }

            var list = new List<int>();

            return Created("",FlightStorage.AddFlight(request));
        }
    }
}
