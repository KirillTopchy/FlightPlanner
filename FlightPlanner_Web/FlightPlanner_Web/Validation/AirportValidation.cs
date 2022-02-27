using FlightPlanner_Web.Models;

namespace FlightPlanner_Web.Validation
{
    public class AirportValidation
    {
        private static readonly object FlightLock = new();
        public static bool AirportIsValid(AddFlightRequest request)
        {
            lock (FlightLock)
            {
                return request.From.AirportName.ToLower().Trim() != request.To.AirportName.ToLower().Trim();
            }
        }
    }
}
