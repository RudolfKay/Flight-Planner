using System.Text.Json.Serialization;

namespace FlightPlanner
{
    public class Airport
    {
        public string Country { get; set; }

        public string City { get; set; }

        [JsonPropertyName("airport")]
        public string AirPortCode { get; set; }
    }
}
