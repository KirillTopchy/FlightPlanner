using System.Linq;
using FlightPlanner_Web.Models;
using FlightPlanner_Web.Storage;
using FlightPlanner_Web.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner_Web.Controllers
{
    [Route("admin-api")]
    [EnableCors]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;

        public AdminApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlights(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);

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
            var flight = _context.Flights
                .Include(f => f.To)
                .Include(f => f.From)
                .SingleOrDefault(f => f.Id == id);

            if (flight != null)
            {
                _context.Flights.Remove(flight);
                _context.SaveChanges();
            }

            return Ok();
        }

        [HttpPut, Authorize]
        [Route("flights")]
        public IActionResult PutFlight(AddFlightRequest request)
        { 
            if (!FlightValidation.FlightIsValid(request) || !AirportValidation.AirportIsValid(request))
            {
                return BadRequest();
            }

            if (FlightStorage.Exists(request, _context))
            {
                return Conflict();
            }

            var flight = FlightStorage.ConvertFlight(request);
            _context.Flights.Add(flight);
            _context.SaveChanges();

            return Created("", flight);
        }

        private bool Exists(AddFlightRequest request)
        {
            return _context.Flights.Any(f =>
                f.Carrier.ToLower().Trim() == request.Carrier.ToLower().Trim() &&
                f.DepartureTime.ToLower().Trim() == request.DepartureTime.ToLower().Trim() &&
                f.ArrivalTime.ToLower().Trim() == request.ArrivalTime.ToLower().Trim() &&
                f.From.AirportName.ToLower().Trim() == request.From.AirportName.ToLower().Trim() &&
                f.To.AirportName.ToLower().Trim() == request.To.AirportName.ToLower().Trim());
        }
    }
}
