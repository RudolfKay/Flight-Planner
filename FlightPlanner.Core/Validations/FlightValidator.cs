using FlightPlanner.Core.Models;
using System;

namespace FlightPlanner.Core.Validations
{
    public class FlightValidator : IFlightValidator
    {
        public bool IsFlightValid(Flight flight)
        {
            //Isn't null or empty
            if (flight.From == null || flight.To == null || string.IsNullOrEmpty(flight.Carrier) ||
                string.IsNullOrEmpty(flight.DepartureTime) || string.IsNullOrEmpty(flight.ArrivalTime))
            {
                return false;
            }

            if (string.IsNullOrEmpty(flight.From.Country) || string.IsNullOrEmpty(flight.From.City) || string.IsNullOrEmpty(flight.From.AirPortCode) ||
                string.IsNullOrEmpty(flight.To.Country) || string.IsNullOrEmpty(flight.To.City) || string.IsNullOrEmpty(flight.To.AirPortCode))
            {
                return false;
            }

            //Flight airports valid - not the same
            if (flight.From.Country.ToLower().Trim() == flight.To.Country.ToLower().Trim() &&
                flight.From.City.ToLower().Trim() == flight.To.City.ToLower().Trim() &&
                flight.From.AirPortCode.ToLower().Trim() == flight.To.AirPortCode.ToLower().Trim())
            {
                return false;
            }

            //Arrival is later than departure
            if (!string.IsNullOrEmpty(flight?.ArrivalTime) &&
                !string.IsNullOrEmpty(flight?.DepartureTime))
            {
                var arrival = DateTime.Parse(flight.ArrivalTime);
                var departure = DateTime.Parse(flight.DepartureTime);

                if (departure >= arrival)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
