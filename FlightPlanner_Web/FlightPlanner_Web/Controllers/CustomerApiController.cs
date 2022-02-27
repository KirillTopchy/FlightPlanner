using System.Collections.Generic;
using System.Linq;
using FlightPlanner_Web.Models;
using FlightPlanner_Web.Storage;
using FlightPlanner_Web.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner_Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        private static readonly object FlightLock = new();

        public CustomerApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            var airports = FlightStorage.FindAirport(search, _context);

                if (airports != null)
                {
                    return Ok(airports);
                }

                return NotFound();
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightRequest request)
        {
            lock (FlightLock)
            {
                if (!FlightValidation.IsValidSearchFlightRequest(request))
                {
                    return BadRequest();
                }

                return Ok(FlightStorage.SearchFlight(request, _context));
            }
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchFlightsById(int id)
        {
            var flight = FlightStorage.GetFlight(id, _context);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
