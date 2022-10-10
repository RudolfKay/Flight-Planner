using System.Collections.Generic;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        void ClearData();

        Flight GetCompleteFlightById(int id);

        bool Exists(Flight flight);

        Airport SearchForAirport(List<Flight> flights, string search);

        PageResult SearchForFlight(SearchFlightsRequest req, List<Flight> flights);
    }
}
