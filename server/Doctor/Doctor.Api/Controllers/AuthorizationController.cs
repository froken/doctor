using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Doctor.Api.Controllers
{
    public class AuthorizationController : Controller
    {
        [HttpGet("~/connect/authorize")]
        public IActionResult Authorize(OpenIdConnectRequest request)
        {
            var claims = new List<Claim>();
            var user = new Claim(OpenIdConnectConstants.Claims.Subject, "user-001");

            claims.Add(user);

            var identity = new ClaimsIdentity(claims, "OpenIddict");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(),
                OpenIdConnectServerDefaults.AuthenticationScheme);

            return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        }
    }
}
