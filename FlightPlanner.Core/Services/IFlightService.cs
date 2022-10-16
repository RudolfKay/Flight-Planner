using System.Collections.Generic;
using FlightPlanner.Core.Models;
using AutoMapper;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        void ClearData();

        Flight GetCompleteFlightById(int id);

        bool Exists(Flight flight);

        Airport SearchForAirport(string search);

        PageResult SearchForFlight(SearchFlightsRequest req);
    }
}
