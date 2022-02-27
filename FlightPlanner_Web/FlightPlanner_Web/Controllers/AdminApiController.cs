using FlightPlanner_Web.Models;
using FlightPlanner_Web.Storage;
using FlightPlanner_Web.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner_Web.Controllers
{
    [Route("admin-api")]
    [EnableCors]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        private static readonly object FlightLock = new();

        public AdminApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlights(int id)
        {
            var flight = FlightStorage.GetFlight(id, _context);

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
            lock (FlightLock)
            {
                FlightStorage.DeleteFlight(id, _context);

                return Ok();
            }
        }

        [HttpPut, Authorize]
        [Route("flights")]
        public IActionResult PutFlight(AddFlightRequest request)
        {
            lock (FlightLock)
            {
                if (!FlightValidation.FlightIsValid(request) || !AirportValidation.AirportIsValid(request))
                {
                    return BadRequest();
                }

                if (FlightStorage.Exists(request, _context))
                {
                    return Conflict();
                }

                return Created("", FlightStorage.AddFlight(request, _context));
            }
        }
    }
}
