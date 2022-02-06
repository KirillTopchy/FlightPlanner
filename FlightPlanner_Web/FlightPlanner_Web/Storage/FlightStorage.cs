﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using FlightPlanner_Web.Controllers;
using FlightPlanner_Web.Models;

namespace FlightPlanner_Web.Storage
{
    public static class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id;

        public static Flight AddFlight(AddFlightRequest request)
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
            _flights.Add(flight);

            return flight;
        }

        public static void ClearFlights()
        {
            _flights.Clear();
            _id = 0;
        }

        public static void DeleteFlight(int id)
        {
            var flight = GetFlight(id);
            if (flight != null)
            {
                _flights.Remove(flight);
            }
        }

        public static Flight GetFlight(int id)
        {
            return _flights.SingleOrDefault(f => f.Id == id);
        }

        public static bool Exists(AddFlightRequest request)
        {
            return _flights.Any(f => 
                f.Carrier.ToLower().Trim() == request.Carrier.ToLower().Trim() &&
                f.DepartureTime.ToLower().Trim() == request.DepartureTime.ToLower().Trim() &&
                f.ArrivalTime.ToLower().Trim() == request.ArrivalTime.ToLower().Trim() &&
                f.From.AirportName.ToLower().Trim() == request.From.AirportName.ToLower().Trim() &&
                f.To.AirportName.ToLower().Trim() == request.To.AirportName.ToLower().Trim());
        }
    }
}
 