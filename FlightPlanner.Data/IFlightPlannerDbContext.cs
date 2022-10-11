using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using FlightPlanner.Core.Models;
using System.Threading.Tasks;

namespace FlightPlanner.Data
{
    public interface IFlightPlannerDbContext
    {
        DbSet<T> Set<T>() where T : class;

        EntityEntry<T> Entry<T>(T entity) where T : class;

        DbSet<Flight> Flights { get; set; }

        DbSet<Airport> Airports { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
