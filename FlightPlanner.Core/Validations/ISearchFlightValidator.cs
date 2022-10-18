namespace FlightPlanner.Core.Validations
{
    public interface ISearchFlightValidator
    {
        public SearchFlightsRequest IsFlightSearchRequestValid(SearchFlightsRequest req);
    }
}
