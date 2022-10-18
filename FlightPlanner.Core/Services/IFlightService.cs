using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        void ClearFlights();

        Flight GetCompleteFlightById(int id);

        bool Exists(Flight flight);

        public PageResult SearchForFlight(SearchFlightsRequest req);
    }
}
