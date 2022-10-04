using System.Collections.Generic;
using System.Linq;
using System;

namespace FlightPlanner
{
    public class FlightStorage
    {
        private readonly FlightPlannerDbContext _context;
        private static readonly List<Flight> _flights = new();
        private static int _id = 0;

        public FlightStorage(FlightPlannerDbContext context)
        {
            _context = context;
        }

        //---------------------Deprecated------------------------//

        public static Flight AddFlight(Flight flight)
        {
            flight.Id = ++_id;
            _flights.Add(flight);

            return flight;
        }

        public static Flight GetFlight(int id)
        {
            return _flights.FirstOrDefault(f => f.Id == id);
        }

        public static void DeleteFlight(Flight flight)
        {
            _flights.Remove(flight);
        }
        
        public static void Clear()
        {
            _flights.Clear();
            _id = 0;
        }

        //-------------------------------------------------------//

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

        public Flight IsFlightValid(Flight flight)
        {
            foreach (Flight temp in _context.Flights)
            {
                if (flight.From.Country.ToLower().Trim() == temp.From.Country.ToLower().Trim() &&
                    flight.From.City.ToLower().Trim() == temp.From.City.ToLower().Trim() &&
                    flight.From.AirPortCode.ToLower().Trim() == temp.From.AirPortCode.ToLower().Trim() &&
                    flight.To.Country.ToLower().Trim() == temp.To.Country.ToLower().Trim() &&
                    flight.To.City.ToLower().Trim() == temp.To.City.ToLower().Trim() &&
                    flight.To.AirPortCode.ToLower().Trim() == temp.To.AirPortCode.ToLower().Trim() &&
                    flight.Carrier.ToLower().Trim() == temp.Carrier.ToLower().Trim() &&
                    flight.DepartureTime.ToLower().Trim() == temp.DepartureTime.ToLower().Trim() &&
                    flight.ArrivalTime.ToLower().Trim() == temp.ArrivalTime.ToLower().Trim())
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

        public Airport SearchForAirport(string search)
        {
            var searchFor = search.ToLower().Trim();

            foreach (Flight f in _context.Flights)
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

        public PageResult SearchForFlight(SearchFlightsRequest req)
        {
            PageResult pr = new();

            if (IsFlightSearchRequestValid(req) == null)
            {
                return null;
            }

            var airportFrom = req.From.ToLower().Trim();
            var airportTo = req.To.ToLower().Trim();

            foreach (Flight f in _context.Flights)
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
