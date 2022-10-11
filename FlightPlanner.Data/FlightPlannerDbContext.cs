using Microsoft.EntityFrameworkCore;
using FlightPlanner.Core.Models;
using System.Threading.Tasks;

namespace FlightPlanner.Data
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
    }
}
