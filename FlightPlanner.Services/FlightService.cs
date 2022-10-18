using FlightPlanner.Core.Validations;
using Microsoft.EntityFrameworkCore;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Models;
using FlightPlanner.Data;
using System.Linq;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        private readonly ISearchFlightValidator _validator;

        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
            _validator = new SearchFlightValidator();
        }

        public void ClearFlights()
        {
            _context.Flights.RemoveRange(_context.Flights);

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

        public PageResult SearchForFlight(SearchFlightsRequest req)
        {
            var validation = _validator.IsFlightSearchRequestValid(req);

            if (validation == null)
            {
                return null;
            }

            var flights = _context.Flights
                    .Include(f => f.From)
                    .Include(f => f.To)
                    .ToList()
                    .Where(f => f.From.AirPortCode.Equals(req.From) && f.To.AirPortCode.Equals(req.To))
                    .ToArray();

            PageResult result = new()
            {
                Page = 0,
                TotalItems = flights.Length,
                Items = flights,
            };

            return result;
        }
    }
}
