using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Validations
{
    public interface IFlightValidator
    {
        public bool IsFlightValid(Flight flight);
    }
}
