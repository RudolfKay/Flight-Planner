using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner
{
    public class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 0;

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

        public static Airport SearchForAirport(string search)
        {
            var searchFor = search.ToLower().Trim();

            foreach (Flight f in _flights)
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

        public static Flight SearchForFlight(Flight flight)
        {
            if (_flights.Count <= 1)
            {
                return null;
            }

            var countryFrom = flight.From.Country.ToLower().Trim();
            var cityFrom = flight.From.City.ToLower().Trim();
            var airportFrom = flight.From.AirPortCode.ToLower().Trim();

            var countryTo = flight.To.Country.ToLower().Trim();
            var cityTo = flight.To.City.ToLower().Trim();
            var airportTo = flight.To.AirPortCode.ToLower().Trim();

            var departureTime = flight.DepartureTime.Trim();

            foreach (Flight f in _flights)
            {
                if (f.From.Country.ToLower().Trim().Equals(countryFrom) &&
                    f.From.City.ToLower().Trim().Equals(cityFrom) &&
                    f.From.AirPortCode.ToLower().Trim().Equals(airportFrom) &&
                    f.To.Country.ToLower().Trim().Equals(countryTo) &&
                    f.To.City.ToLower().Trim().Equals(cityTo) &&
                    f.To.AirPortCode.ToLower().Trim().Equals(airportTo) &&
                    f.DepartureTime.Trim().Equals(departureTime))

                /*if (f.From == flight.From &&
                    f.To == flight.To &&
                    f.DepartureTime == flight.DepartureTime)*/
                {
                    return f;
                }
            }

            return null;
        }

        public static void DeleteFlight(Flight flight)
        {
            _flights.Remove(flight);
        }

        public static Flight IsFlightNullOrEmpty(Flight flight)
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

        public static Flight IsAirportValid(Flight flight)
        {
            if (flight.From.Country.ToLower().Trim() == flight.To.Country.ToLower().Trim() &&
                flight.From.City.ToLower().Trim() == flight.To.City.ToLower().Trim() &&
                flight.From.AirPortCode.ToLower().Trim() == flight.To.AirPortCode.ToLower().Trim())
            {
                return null;
            }

            return flight;
        }

        public static Flight IsFlightValid(Flight flight)
        {
            foreach (Flight temp in _flights)
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

        public static Flight IsTimeValid(Flight flight)
        {
            var departure = DateTime.Parse(flight.DepartureTime);
            var arrival = DateTime.Parse(flight.ArrivalTime);

            if (departure >= arrival)
            {
                return null;
            }

            return flight;
        }

        public static void Clear()
        {
            _flights.Clear();
            _id = 0;
        }
    }
}
