using System;
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
        private readonly object storageLock = new object();

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlights(int id)
        {
            lock (storageLock)
            {
                var flight = FlightStorage.GetFlight(id);
                if (flight == null)
                {
                    return NotFound();
                }

                return Ok(flight);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (storageLock)
            {
                FlightStorage.DeleteFlight(id);
                return Ok();
            }
        }

        [HttpPut, Authorize]
        [Route("flights")]
        public IActionResult PutFlight(AddFlightRequest request)
        {
            lock (storageLock)
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

                return Created("", FlightStorage.AddFlight(request));
            }
        }
    }
}
