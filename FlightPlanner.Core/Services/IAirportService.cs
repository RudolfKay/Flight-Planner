using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IAirportService : IEntityService<Airport>
    {
        void ClearAirports();

        Airport SearchForAirport(string search);
    }
}
