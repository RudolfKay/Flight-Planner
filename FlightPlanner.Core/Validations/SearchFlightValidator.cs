namespace FlightPlanner.Core.Validations
{
    public class SearchFlightValidator : ISearchFlightValidator
    {
        public SearchFlightsRequest IsFlightSearchRequestValid(SearchFlightsRequest req)
        {
            if (req == null ||
                string.IsNullOrEmpty(req.From) ||
                string.IsNullOrEmpty(req.To) ||
                req.From == req.To)
            {
                return null;
            }

            return req;
        }
    }
}
