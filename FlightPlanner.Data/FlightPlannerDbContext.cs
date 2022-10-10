using FlightPlanner.Core.Models;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FlightPlanner
{
    public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
    {
        public FlightPlannerDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Airport> Airports { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        internal Flight FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
