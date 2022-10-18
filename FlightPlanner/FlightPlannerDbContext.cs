using Microsoft.EntityFrameworkCore;
using System;

namespace FlightPlanner
{
    public class FlightPlannerDbContext : DbContext
    {
        public FlightPlannerDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Airport> Airports { get; set; }

        internal Flight FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
