using FlightPlanner.Core.Models;
using FlightPlanner.Models;
using AutoMapper;
using System;

namespace FlightPlanner
{
    public class AutoMapperConfig
    {
        public static IMapper CreateMapper()
        {
            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            var config = new MapperConfiguration
                (cfg => {cfg.CreateMap<AirportRequest, Airport>().
                    ForMember(d => d.Id, options => options.Ignore()).
                    ForMember(d => d.AirPortCode, opt => opt.MapFrom(s => s.Airport));
                cfg.CreateMap<Airport, AirportRequest>().
                    ForMember(d => d.Airport, opt => opt.MapFrom(s => s.AirPortCode));
                cfg.CreateMap<FlightRequest, Flight>();
                cfg.CreateMap<Flight, FlightRequest>();
                });

            if (isDevelopment)
            {
                config.AssertConfigurationIsValid();
            }

            return config.CreateMapper();
        }
    }
}
