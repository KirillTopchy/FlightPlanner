using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using FlightPlanner_Web.Controllers;
using FlightPlanner_Web.Models;

namespace FlightPlanner_Web.Storage
{
    public static class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id;
        public static bool flightExist { get; set; }

        public static Flight AddFlight(AddFlightRequest request)
        {
            flightExist = false;
            var flight = new Flight
            {
                From = request.From,
                To = request.To,
                ArrivalTime = request.ArrivalTime,
                DepartureTime = request.DepartureTime,
                Carrier = request.Carrier,
                Id = ++_id
            };

            if (_id != 1 && !_flights.Exists(x => x.From.ToString() == flight.From.ToString()) &&
                !_flights.Exists(x => x.To.ToString() == flight.To.ToString()) &&
                !_flights.Exists(x => x.ArrivalTime == flight.ArrivalTime) &&
                !_flights.Exists(x => x.DepartureTime == flight.DepartureTime) &&
                !_flights.Exists(x => x.Carrier == flight.Carrier))
            {
                _flights.Add(flight);
            }
            else
            {
                flightExist = true;
            }

            return flight;
        }

        public static void ClearFlights()
        {
            _flights.Clear();
            _id = 0;
        }
    }
}
 