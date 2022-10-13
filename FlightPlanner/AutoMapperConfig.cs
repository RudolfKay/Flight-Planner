using FlightPlanner.Core.Models;
using FlightPlanner.Models;
using AutoMapper;

namespace FlightPlanner
{
    public class AutoMapperConfig
    {

        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration
                (cfg => {cfg.CreateMap<AirportRequest, Airport>().
                    ForMember(d => d.Id, options => options.Ignore()).
                    ForMember(d => d.AirPortCode, opt => opt.MapFrom(s => s.Airport));
                cfg.CreateMap<Airport, AirportRequest>().
                    ForMember(d => d.Airport, opt => opt.MapFrom(s => s.AirPortCode));
                cfg.CreateMap<FlightRequest, Flight>();
                cfg.CreateMap<Flight, FlightRequest>();
                });

            //only in development, not for final product. Checks if config is valid 1:1
            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}
