using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using WebApiProject.Repository;
using WebApiProject.Models;

namespace WebApiProject.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public readonly IUserRepository _userRepository;
        public BasicAuthenticationHandler(Microsoft.Extensions.Options.IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUserRepository userRepository)
        : base(options, logger, encoder, clock)
        {
            _userRepository = userRepository;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");
            User user=null;
            try
            {
                var authHead = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var cerdentialsBytes = Convert.FromBase64String(authHead.Parameter);
                var cerdentials = Encoding.UTF8.GetString(cerdentialsBytes).Split(":", 2);
                var username = cerdentials[0];
                var password = cerdentials[1];
                user = await _userRepository.Authenticate(username,password);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
            if (user == null)
                return AuthenticateResult.Fail("Invalid Username or Password");
             var claims = new[] {
                new Claim(ClaimTypes.Name, user.Username),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}