using System;
using FlightPlanner_Web.Models;

namespace FlightPlanner_Web.Validation
{
    public class FlightValidation
    {
        public static bool FlightIsValid(AddFlightRequest request)
        {
            if (request == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(request.ArrivalTime) || string.IsNullOrEmpty(request.Carrier) ||
                string.IsNullOrEmpty(request.DepartureTime))
            {
                return false;
            }

            if (request.From == null || request.To == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(request.From.AirportName) || string.IsNullOrEmpty(request.From.City) ||
                string.IsNullOrEmpty(request.From.Country))
            {
                return false;
            }

            if (string.IsNullOrEmpty(request.To.AirportName) || string.IsNullOrEmpty(request.To.City) ||
                string.IsNullOrEmpty(request.To.Country))
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

        public static bool IsValidSearchFlightRequest(SearchFlightRequest search)
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

            return true;
        }
    }
}
