using Microsoft.EntityFrameworkCore;
using FlightPlanner.Core.Services;
using System.Collections.Generic;
using FlightPlanner.Core.Models;
using FlightPlanner.Data;
using System.Linq;
using AutoMapper;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public void ClearData()
        {
            _context.Flights.RemoveRange(_context.Flights);
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();
        }

        public Flight GetCompleteFlightById(int id)
        {
            return _context.Flights.
                Include(f => f.From).
                Include(f => f.To).
                SingleOrDefault(f => f.Id == id);
        }

        public bool Exists(Flight flight)
        {
            if (flight == null) return false;

            return _context.Flights.Any(f => f.ArrivalTime == flight.ArrivalTime
            && f.DepartureTime == flight.DepartureTime
            && f.Carrier == flight.Carrier
            && f.From.AirPortCode == flight.From.AirPortCode
            && f.To.AirPortCode == flight.To.AirPortCode);
        }

        public Airport SearchForAirport(List<Flight> flights, string search, IMapper mapper)
        {
            var searchFor = search.ToLower().Trim();

            foreach (Flight f in flights)
            {
                var flight = mapper.Map<Flight>(f);

                if (flight.From.Country.ToLower().Trim().Contains(searchFor) ||
                    flight.From.City.ToLower().Trim().Contains(searchFor) ||
                    flight.From.AirPortCode.ToLower().Trim().Contains(searchFor))
                {
                    Airport airport = flight.From;

                    return airport;
                }
                if (flight.To.Country.ToLower().Trim().Contains(searchFor) ||
                    flight.To.City.ToLower().Trim().Contains(searchFor) ||
                    flight.To.AirPortCode.ToLower().Trim().Contains(searchFor))
                {
                    Airport airport = flight.To;

                    return airport;
                }
            }

            return null;
        }

        public PageResult SearchForFlight(SearchFlightsRequest req, List<Flight> flights, IMapper mapper)
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
                var flight = mapper.Map<Flight>(f);

                if (
                    flight.From.AirPortCode.ToLower().Trim().Equals(airportFrom) &&
                    flight.To.AirPortCode.ToLower().Trim().Equals(airportTo))
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
