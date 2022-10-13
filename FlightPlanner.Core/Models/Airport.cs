using System.Text.Json.Serialization;

namespace FlightPlanner.Core.Models
{
    public class Airport : Entity
    {
        public string Country { get; set; }

        public string City { get; set; }

        //[JsonPropertyName("airport")]
        public string AirPortCode { get; set; }
    }
}
