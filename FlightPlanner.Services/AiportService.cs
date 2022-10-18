using FlightPlanner.Core.Services;
using FlightPlanner.Core.Models;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public void ClearAirports()
        {
            _context.Airports.RemoveRange(_context.Airports);

            _context.SaveChanges();
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
    }
}
