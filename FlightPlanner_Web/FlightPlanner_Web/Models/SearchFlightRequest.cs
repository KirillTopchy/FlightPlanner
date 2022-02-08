using System.Linq;
using FlightPlanner_Web.Storage;

namespace FlightPlanner_Web.Models
{
    public class SearchFlightRequest
    {
        public string From { get; set; }

        public string To { get; set; }

        public string DepartureDate { get; set; }
    }
}
