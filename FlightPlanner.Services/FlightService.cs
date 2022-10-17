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

        public Airport SearchForAirport(string search)
        {
            var searchFor = search.ToLower().Trim();

            foreach (Airport a in _context.Airports)
            {
                if (a.Country.ToLower().Trim().Contains(searchFor) ||
                    a.City.ToLower().Trim().Contains(searchFor) ||
                    a.AirPortCode.ToLower().Trim().Contains(searchFor))
                {
                    Airport airport = a;

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

            var flights = _context.Flights
                    .Include(f => f.From)
                    .Include(f => f.To)
                    .ToList()
                    .Where(f => f.From.AirPortCode.Equals(req.From) && f.To.AirPortCode.Equals(req.To))
                    .ToArray();

            pr.Items = flights;
            pr.TotalItems = flights.Length;

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
