using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FlightPlanner.Filters;
using FlightPlanner.Core.Services;
using FlightPlanner.Services;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Validations;
using FlightPlanner.Data;
using AutoMapper;

namespace FlightPlanner
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlightPlanner", Version = "v1" });
            });

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthorizationHandler>("BasicAuthentication", null);

            services.AddDbContext<FlightPlannerDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("flight-planner"));
            });

            services.AddScoped<IFlightPlannerDbContext, FlightPlannerDbContext>();
            services.AddScoped<IDbService, DbService>();
            services.AddScoped<IEntityService<Flight>, EntityService<Flight>>();
            services.AddScoped<IEntityService<Airport>, EntityService<Airport>>();
            services.AddScoped<IEntityService<User>, EntityService<User>>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<IFlightValidator, FlightValidator>();
            services.AddScoped<ISearchFlightValidator, SearchFlightValidator>();
            services.AddSingleton<IMapper>(AutoMapperConfig.CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightPlanner v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
