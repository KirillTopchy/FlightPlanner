using FlightPlanner_Web.Models;

namespace FlightPlanner_Web.Validation
{
    public class AirportValidation
    {
        public static bool AirportIsValid(AddFlightRequest request)
        {
            if (request.From.Country.ToLower().Trim() == request.To.Country.ToLower().Trim() ||
                request.From.City.ToLower().Trim() == request.To.City.ToLower().Trim() ||
                request.From.AirportName.ToLower().Trim() == request.To.AirportName.ToLower().Trim())
            {
                return false;
            }

            return true;
        }
    }
}
