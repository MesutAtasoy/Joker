using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace Joker.Identity.Constants
{
    public static class IdentityConfig
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new ("roles", "Your role(s)", new List<string>() {"role"})
            };

        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope("campaignapi", "Campaign Api"),
            new ApiScope("merchantapi", "Merchant Api"),
            new ApiScope("aggregatorapi", "Aggregator Api"),
            
        };
        
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ("merchantapi", "Merchant API", new[] {"role"})
                {
                    Scopes = { "merchantapi"},
                    ApiSecrets = {new Secret("apisecret".Sha256())}
                },
                new ("campaignapi", "Campaign API", new[] {"role"})
                {
                    Scopes = { "campaignapi"},
                    ApiSecrets = {new Secret("apisecret".Sha256())}
                },
                new ("managementapi", "Management API")
                {
                    ApiSecrets = {new Secret("apisecret".Sha256())}
                },
                new ("locationapi", "Location API")
                {
                    ApiSecrets = {new Secret("apisecret".Sha256())}
                },
                new ("searchapi", "Search API")
                {
                    ApiSecrets = {new Secret("apisecret".Sha256())}
                },
                new ("aggregatorapi", "Aggregator API")
                {
                    Scopes = { "aggregatorapi"},
                    ApiSecrets = {new Secret("apisecret".Sha256())}
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new ()
                {
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 120,
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    ClientName = "Joker Web Application",
                    ClientId = "joker.web.app",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:5030/signin-oidc",
                        "https://joker.webapp/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:5030/signout-callback-oidc",
                        "https://joker.webapp/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "merchantapi",
                        "campaignapi",
                        "managementapi",
                        "locationapi",
                        "searchapi",
                        "aggregatorapi"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                }
            };
    }
}