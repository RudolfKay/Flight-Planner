using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Encodings.Web;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace FlightPlanner.Filters
{
    public class BasicAuthorizationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IEntityService<User> _userService;

        public BasicAuthorizationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                         ILoggerFactory logger,
                                         UrlEncoder encoder,
                                         ISystemClock clock,
                                         IEntityService<User> userService)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // skip authentication if endpoint has [AllowAnonymous] attribute
            var endpoint = Context.GetEndpoint();

            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            bool authorized;

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];

                /*var password = Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(
                        credentials[1].ToCharArray()
                        )
                    );
                */
                
                //authorized = _userService.Query().Any(u => u.Username == username && u.Password == password); 

                authorized = (username == "codelex-admin" && password == "Password123");
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (!authorized)
                return AuthenticateResult.Fail("Invalid Username or Password");

            var claims = new[] {
                new Claim(ClaimTypes.Name, "user"),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
