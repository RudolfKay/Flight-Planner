using Microsoft.EntityFrameworkCore;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Models;
using System.Linq;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService<Flight>
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
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
            return _context.Flights.Any(f => f.ArrivalTime == flight.ArrivalTime &&
                                 f.DepartureTime == flight.DepartureTime &&
                                 f.Carrier == flight.Carrier &&
                                 f.From.AirPortCode == flight.From.AirPortCode &&
                                 f.To.AirPortCode == flight.To.AirPortCode);
        }
    }
}
