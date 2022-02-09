using System;
using FlightPlanner_Web.Models;

namespace FlightPlanner_Web.Validation
{
    public class FlightValidation
    {
        private static readonly object FlightLock = new();

        public static bool FlightIsValid(AddFlightRequest request)
        {
            lock (FlightLock)
            {
                if (request == null || request.From == null || request.To == null)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(request.ArrivalTime) || string.IsNullOrEmpty(request.Carrier) ||
                    string.IsNullOrEmpty(request.DepartureTime) || string.IsNullOrEmpty(request.From.AirportName) || 
                    string.IsNullOrEmpty(request.To.AirportName))
                {
                    return false;
                }

                var arrivalTime = DateTime.Parse(request.ArrivalTime);
                var departureTime = DateTime.Parse(request.DepartureTime);

                if (arrivalTime <= departureTime)
                {
                    return false;
                }

                return true;
            }
        }

        public static bool IsValidSearchFlightRequest(SearchFlightRequest search)
        {
            lock (FlightLock)
            {
                if (search == null)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(search.From) ||
                    string.IsNullOrEmpty(search.To) ||
                    string.IsNullOrEmpty(search.DepartureDate))
                {
                    return false;
                }

                if (search.From == search.To)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
