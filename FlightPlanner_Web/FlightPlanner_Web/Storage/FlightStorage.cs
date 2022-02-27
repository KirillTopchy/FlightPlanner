using System.Collections.Generic;
using System.Linq;
using FlightPlanner_Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner_Web.Storage
{
    public static class FlightStorage
    {
        private static readonly object FlightLock = new();

        public static Flight AddFlight(AddFlightRequest request, FlightPlannerDbContext context)
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
                };

                context.Flights.Add(flight);
                context.SaveChanges();

                return flight;
            }
        }

        public static void ClearFlights(FlightPlannerDbContext context)
        {
            context.RemoveRange(context.Flights);
            context.RemoveRange(context.Airports);
            context.SaveChanges();
        }

        public static void DeleteFlight(int id, FlightPlannerDbContext context)
        {
            lock (FlightLock)
            {
                var flight = GetFlight(id, context);

                if (flight != null)
                { 
                    context.Flights.Remove(flight);
                    context.SaveChanges();
                }
            }
        }

        public static Flight GetFlight(int id, FlightPlannerDbContext context)
        {
            return context.Flights
                    .Include(f => f.From)
                    .Include(f=> f.To)
                    .SingleOrDefault(f => f.Id == id);
        }

        public static bool Exists(AddFlightRequest request, FlightPlannerDbContext context)
        {
            lock (FlightLock)
            {
                return context.Flights.Any(f =>
                    f.Carrier.ToLower().Trim() == request.Carrier.ToLower().Trim() &&
                    f.DepartureTime.ToLower().Trim() == request.DepartureTime.ToLower().Trim() &&
                    f.ArrivalTime.ToLower().Trim() == request.ArrivalTime.ToLower().Trim() &&
                    f.From.AirportName.ToLower().Trim() == request.From.AirportName.ToLower().Trim() &&
                    f.To.AirportName.ToLower().Trim() == request.To.AirportName.ToLower().Trim());
            }
        }

        public static List<Airport> FindAirport(string userInput, FlightPlannerDbContext context)
        {
            var fromAirportsList = context.Flights.Where(a =>
                        a.From.AirportName.ToLower().Trim().Contains(userInput.ToLower().Trim()) ||
                        a.From.City.ToLower().Trim().Contains(userInput.ToLower().Trim()) ||
                        a.From.Country.ToLower().Trim().Contains(userInput.ToLower().Trim()))
                    .Select(a => a.From).ToList();

            var toAirportsList = context.Flights.Where(a =>
                        a.To.AirportName.ToLower().Trim().Contains(userInput.ToLower().Trim()) ||
                        a.To.City.ToLower().Trim().Contains(userInput.ToLower().Trim()) ||
                        a.To.Country.ToLower().Trim().Contains(userInput.ToLower().Trim()))
                    .Select(a => a.To).ToList();

            return fromAirportsList.Concat(toAirportsList).ToList();
        }

        public static PageResult SearchFlight(SearchFlightRequest req, FlightPlannerDbContext context)
        {
            lock (FlightLock)
            {
                var foundFlight = context.Flights
                    .Include(f => f.From)
                    .Include(f => f.To)
                    .Where(f =>
                        f.From.AirportName.ToLower().Trim() == req.From.ToLower().Trim() &&
                        f.To.AirportName.ToLower().Trim() == req.To.ToLower().Trim() &&
                        f.DepartureTime.Substring(0, 10) == req.DepartureDate.Substring(0, 10)).ToList();

                return new PageResult(foundFlight);
            }
        }
    }
}
 