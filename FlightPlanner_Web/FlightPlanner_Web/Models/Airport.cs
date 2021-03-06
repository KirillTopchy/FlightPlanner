using System.Text.Json.Serialization;

namespace FlightPlanner_Web.Models
{
    public class Airport
    {
        public string Country { get; set; }

        public string City { get; set; }

        [JsonPropertyName("airport")]
        public string AirportName { get; set; }
    }
}
