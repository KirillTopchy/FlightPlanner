using System.Linq;
using FlightPlanner_Web.Storage;

namespace FlightPlanner_Web.Models
{
    public class SearchFlightRequest
    {
        public string From { get; set; }

        public string To { get; set; }

        public string DepartureDate { get; set; }

        public static PageResult FindFlights(SearchFlightRequest search)
        {
            var flight = FlightStorage.GetFlightsList()
                .Where(f => f.From.AirportName == search.From ||
                               f.To.AirportName == search.To ||
                               f.DepartureTime == search.DepartureDate).ToList();

            return new PageResult(flight);
        }
    }
}
