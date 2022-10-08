using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FlightPlanner
{
    public class FlightStorage
    {
        public Flight GetFlight(int id, FlightPlannerDbContext _context)
        {
            return _context.Flights.
                Include(f => f.From).
                Include(f => f.To).
                FirstOrDefault(f => f.Id == id);
        }

        public Flight IsFlightNullOrEmpty(Flight flight)
        {
            if (flight.From == null || flight.To == null || string.IsNullOrEmpty(flight.Carrier) ||
                string.IsNullOrEmpty(flight.DepartureTime) || string.IsNullOrEmpty(flight.ArrivalTime))
            {
                return null;
            }

            if (string.IsNullOrEmpty(flight.From.Country) || string.IsNullOrEmpty(flight.From.City) || string.IsNullOrEmpty(flight.From.AirPortCode) ||
                string.IsNullOrEmpty(flight.To.Country) || string.IsNullOrEmpty(flight.To.City) || string.IsNullOrEmpty(flight.To.AirPortCode))
            {
                return null;
            }

            return flight;
        }

        public Flight IsAirportValid(Flight flight)
        {
            if (flight.From.Country.ToLower().Trim() == flight.To.Country.ToLower().Trim() &&
                flight.From.City.ToLower().Trim() == flight.To.City.ToLower().Trim() &&
                flight.From.AirPortCode.ToLower().Trim() == flight.To.AirPortCode.ToLower().Trim())
            {
                return null;
            }

            return flight;
        }

        public Flight IsFlightValid(List<Flight> flights, Flight flight)
        {
            foreach (Flight f in flights)
            {
                if (flight.From.Country.ToLower().Trim() == f.From.Country.ToLower().Trim() &&
                    flight.From.City.ToLower().Trim() == f.From.City.ToLower().Trim() &&
                    flight.From.AirPortCode.ToLower().Trim() == f.From.AirPortCode.ToLower().Trim() &&
                    flight.To.Country.ToLower().Trim() == f.To.Country.ToLower().Trim() &&
                    flight.To.City.ToLower().Trim() == f.To.City.ToLower().Trim() &&
                    flight.To.AirPortCode.ToLower().Trim() == f.To.AirPortCode.ToLower().Trim() &&
                    flight.Carrier.ToLower().Trim() == f.Carrier.ToLower().Trim() &&
                    flight.DepartureTime.ToLower().Trim() == f.DepartureTime.ToLower().Trim() &&
                    flight.ArrivalTime.ToLower().Trim() == f.ArrivalTime.ToLower().Trim())
                {
                    return null;
                }
            }

            return flight;
        }

        public Flight IsTimeValid(Flight flight)
        {
            var departure = DateTime.Parse(flight.DepartureTime);
            var arrival = DateTime.Parse(flight.ArrivalTime);

            if (departure >= arrival)
            {
                return null;
            }

            return flight;
        }

        public Airport SearchForAirport(List<Flight> flights, string search)
        {
            var searchFor = search.ToLower().Trim();

            foreach (Flight f in flights)
            {
                if (f.From.Country.ToLower().Trim().Contains(searchFor) ||
                    f.From.City.ToLower().Trim().Contains(searchFor) ||
                    f.From.AirPortCode.ToLower().Trim().Contains(searchFor))
                {
                    Airport airport = f.From;

                    return airport;
                }
                if (f.To.Country.ToLower().Trim().Contains(searchFor) ||
                    f.To.City.ToLower().Trim().Contains(searchFor) ||
                    f.To.AirPortCode.ToLower().Trim().Contains(searchFor))
                {
                    Airport airport = f.To;

                    return airport;
                }
            }

            return null;
        }

        public PageResult SearchForFlight(SearchFlightsRequest req, List<Flight> flights)
        {
            PageResult pr = new();

            if (IsFlightSearchRequestValid(req) == null)
            {
                return null;
            }

            var airportFrom = req.From.ToLower().Trim();
            var airportTo = req.To.ToLower().Trim();

            foreach (Flight f in flights)
            {
                if (
                    f.From.AirPortCode.ToLower().Trim().Equals(airportFrom) &&
                    f.To.AirPortCode.ToLower().Trim().Equals(airportTo))
                {
                    pr.Items.Add(f);
                    pr.TotalItems++;
                }
            }

            return pr;
        }

        public static SearchFlightsRequest IsFlightSearchRequestValid(SearchFlightsRequest req)
        {
            if (req == null ||
                string.IsNullOrEmpty(req.From) ||
                string.IsNullOrEmpty(req.To) ||
                req.From == req.To)
            {
                return null;
            }

            return req;
        }
    }
}
