using FlightPlanner.Core.Models;

namespace FlightPlanner_Web.Models
{
    public class Airport : Entity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string AirportName { get; set; }
    }
}
