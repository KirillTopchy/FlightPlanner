using System.Collections.Generic;
using System.Linq;
using FlightPlanner_Web.Models;

namespace FlightPlanner_Web.Storage
{
    public static class FlightStorage
    {
        private static readonly object FlightLock = new();
        private static readonly List<Flight> Flights = new();
        private static int _id;
        

        public static Flight AddFlight(AddFlightRequest request)
        {
            lock (FlightLock)
            {
                var flight = new Flight
                {
                    From = request.From,
                    To = request.To,
                    ArrivalTime = request.ArrivalTime,
                    DepartureTime = request.DepartureTime,
                    Carrier = request.Carrier,
                    Id = ++_id
                };

                Flights.Add(flight);
            
                return flight;
            }
        }

        public static Flight ConvertFlight(AddFlightRequest request)
        {
            var flight = new Flight
            {
                From = request.From,
                To = request.To,
                ArrivalTime = request.ArrivalTime,
                DepartureTime = request.DepartureTime,
                Carrier = request.Carrier,
            };
            return flight;
        }

        public static void ClearFlights()
        {
            lock (FlightLock)
            {
                Flights.Clear();
            }
            _id = 0;
        }

        public static void DeleteFlight(int id)
        { 
            var flight = GetFlight(id);
            if (flight != null)
            {
                lock (FlightLock)
                {
                    Flights.Remove(flight);
                }
            }
        }

        public static Flight GetFlight(int id)
        {
            lock (FlightLock)
            {
                return Flights.SingleOrDefault(f => f.Id == id);
            }
        }

        public static bool Exists(AddFlightRequest request)
        {
            lock (FlightLock)
            {
                return Flights.Any(f =>
                    f.Carrier.ToLower().Trim() == request.Carrier.ToLower().Trim() &&
                    f.DepartureTime.ToLower().Trim() == request.DepartureTime.ToLower().Trim() &&
                    f.ArrivalTime.ToLower().Trim() == request.ArrivalTime.ToLower().Trim() &&
                    f.From.AirportName.ToLower().Trim() == request.From.AirportName.ToLower().Trim() &&
                    f.To.AirportName.ToLower().Trim() == request.To.AirportName.ToLower().Trim());
            }
        }

        public static List<Airport> FindAirport(string userInput)
        {
            lock (FlightLock)
            {
                var fromAirportsList = Flights.Where(a =>
                        a.From.AirportName.ToLower().Trim().Contains(userInput.ToLower().Trim()) ||
                        a.From.City.ToLower().Trim().Contains(userInput.ToLower().Trim()) ||
                        a.From.Country.ToLower().Trim().Contains(userInput.ToLower().Trim()))
                    .Select(a => a.From).ToList();

                var toAirportsList = Flights.Where(a =>
                        a.To.AirportName.ToLower().Trim().Contains(userInput.ToLower().Trim()) ||
                        a.To.City.ToLower().Trim().Contains(userInput.ToLower().Trim()) ||
                        a.To.Country.ToLower().Trim().Contains(userInput.ToLower().Trim()))
                    .Select(a => a.To).ToList();

                return fromAirportsList.Concat(toAirportsList).ToList();
            }
        }

        public static PageResult SearchFlight(SearchFlightRequest req)
        {
            lock (FlightLock)
            {
                return new PageResult(Flights);
            }
        }
    }
}
 