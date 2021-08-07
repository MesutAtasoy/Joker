using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Joker.WebApp.Extensions
{
    public static class OpenIdConnectExtensions
    {
        public static Task MapRoles(this UserInformationReceivedContext context)
        {
            if (!context.User.RootElement.TryGetProperty(JwtClaimTypes.Role, out var jsonRole))
                return Task.CompletedTask;

            var id = context.Principal.Identity as ClaimsIdentity;

            // var roleClaim = id?.Claims.FirstOrDefault(x => x.Type == "role");
            // id?.RemoveClaim(roleClaim);

            var role = jsonRole.ToString();

            var claims = new List<Claim>();
            if (jsonRole.ValueKind != JsonValueKind.Array)
            {
                if (!string.IsNullOrEmpty(role))
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, role));
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(role))
                {
                    var roles = JsonSerializer.Deserialize<List<string>>(role);
                    if (roles != null && roles.Any())
                    {
                        claims.AddRange(roles.Select(r => new Claim(JwtClaimTypes.Role, r)));
                    }
                }
            }

            if (context.Principal == null)
            {
                return Task.CompletedTask;
            }

            id?.AddClaims(claims);

            return Task.CompletedTask;
        }

        public static Task RedirectToIdentityProvider(this RedirectContext context, string url)
        {
            context.ProtocolMessage.IssuerAddress = $"{url}/connect/authorize";
            return Task.CompletedTask;
        }

        public static Task RedirectToIdentityProviderForSignOut(this RedirectContext context, string url)
        {
            context.ProtocolMessage.IssuerAddress = $"{url}/connect/endsession";
            return Task.CompletedTask;
        }
    }
}