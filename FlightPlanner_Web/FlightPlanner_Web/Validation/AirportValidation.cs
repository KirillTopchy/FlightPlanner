using FlightPlanner_Web.Models;

namespace FlightPlanner_Web.Validation
{
    public class AirportValidation
    {
        public static bool AirportIsValid(AddFlightRequest request)
        {
            return request.From.AirportName.ToLower().Trim() != request.To.AirportName.ToLower().Trim();
        }
    }
}
