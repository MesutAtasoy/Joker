using IdentityServer4;
using IdentityServer4.Models;

namespace Joker.Identity.Constants;

public static class IdentityConfig
{
    public static IEnumerable<IdentityResource> Ids =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new ("roles", "Your role(s)", new List<string>() {"role"}),
            new ("organization", "Your Organization Info(s)", new List<string>() {"organizationId", "organizationName"})
        };

    public static IEnumerable<ApiScope> ApiScopes => new[]
    {
        new ApiScope("campaign", "Campaign Management"),
        new ApiScope("merchant", "Merchant Management"),
        new ApiScope("subscription", "Subscription Management"),
        
        new ApiScope("favorite.create", "Creates Campaign And Store favorites"),
        new ApiScope("favorite.read", "Reads Campaign And Store favorites")
    };
        
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ("merchantapi", "Merchant API", new[] {"role" ,"organizationId","organizationName"})
            {
                Scopes = { "merchant"},
                ApiSecrets = {new Secret("apisecret".Sha256())}
            },
            new ("campaignapi", "Campaign API", new[] {"role", "organizationId","organizationName"})
            {
                Scopes = { "campaign"},
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
            new ("favoriteapi", "Favorite API")
            {
                Scopes = {"favorite.create", "favorite.read"},
                ApiSecrets = {new Secret("apisecret".Sha256())}
            },
            new ("searchapi", "Search API")
            {
                ApiSecrets = {new Secret("apisecret".Sha256())}
            },
            new ("subscriptionapi", "Subscription API")
            {
                Scopes = {"subscription"},
                ApiSecrets = {new Secret("apisecret".Sha256())}
            },
            new ("aggregatorapi", "Aggregator API")
            {
                Scopes = { "campaign", "merchant"},
                ApiSecrets = {new Secret("apisecret".Sha256())}
            }
        };

    public static IEnumerable<Client> Clients(IConfiguration configuration)
    {
        var jokerWebAppUrl = configuration.GetValue<string>("JokerWebAppUrl");
        return new Client[]
        {
            new ()
            {
                AccessTokenType = AccessTokenType.Reference,
                AccessTokenLifetime = 3600,
                AllowOfflineAccess = true,
                UpdateAccessTokenClaimsOnRefresh = true,
                ClientName = "Joker Web Application",
                ClientId = "joker.web.app",
                AllowedGrantTypes = GrantTypes.Code,
                RequireConsent = false,
                RequirePkce = true,
                RedirectUris = new List<string>()
                {
                    $"{jokerWebAppUrl}/signin-oidc",
                },
                PostLogoutRedirectUris = new List<string>()
                {
                    $"{jokerWebAppUrl}/signout-callback-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "roles",
                    "favoriteapi",
                    "searchapi",
                    "favorite.read",
                    "favorite.create"
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                }
            }
        };
    }
}