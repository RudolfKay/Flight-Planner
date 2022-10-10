using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService<T> : IEntityService<Flight>
    {
        Flight GetCompleteFlightById(int id);
        bool Exists(Flight flight);
    }
}
