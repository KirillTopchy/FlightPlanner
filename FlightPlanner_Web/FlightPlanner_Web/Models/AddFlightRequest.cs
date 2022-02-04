﻿namespace FlightPlanner_Web.Models
{
    public class AddFlightRequest
    {
        public string Carrier { get; set; }

        public Airport From { get; set; }

        public Airport To { get; set; }

        public string DepartureTime { get; set; }

        public string ArrivalTime { get; set; }

    }
}
