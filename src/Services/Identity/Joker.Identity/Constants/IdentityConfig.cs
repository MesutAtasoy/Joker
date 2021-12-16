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
            new("roles", "Your role(s)", new List<string>() { "role" }),
            new("organization", "Your Organization Info(s)", new List<string>() { "organizationId", "organizationName" })
        };

    public static IEnumerable<ApiScope> ApiScopes => new[]
    {
        new ApiScope("merchant", "Merchant Management"),
        new ApiScope("campaign", "Campaign Management"),
        new ApiScope("favorite", "Favorite Management"),
        new ApiScope("notification", "Notification Management"),
        new ApiScope("notificationhub", "Real Time Notification Management"),
        new ApiScope("subscription", "Subscription Management"),
        new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
    };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new("merchantapi", "Merchant API", new[] { "role", "organizationId", "organizationName" })
            {
                ApiSecrets = { new Secret("apisecret".Sha256()) },
                Scopes = new List<string> { "merchant" }
            },
            new("campaignapi", "Campaign API", new[] { "role", "organizationId", "organizationName" })
            {
                ApiSecrets = { new Secret("apisecret".Sha256()) },
                Scopes = new List<string> { "campaign" }
            },
            new("favoriteapi", "Favorite API", new[] { "role" })
            {
                ApiSecrets = { new Secret("apisecret".Sha256()) },
                Scopes = new List<string> { "favorite" }
            },
            new("notificationapi", "Notification API")
            {
                ApiSecrets = { new Secret("apisecret".Sha256()) },
                Scopes = new List<string> { "notification" }
            },
            new("aggregatorapi", "Aggregator API", new[] { "role", "organizationId", "organizationName" })
            {
                ApiSecrets = { new Secret("apisecret".Sha256()) },
                Scopes = new List<string> { "merchant", "campaign", "notification", "notificationhub" }
            },
            new("aggregatorstorefrontapi", "Aggregator Store API", new[] { "role" })
            {
                ApiSecrets = { new Secret("apisecret".Sha256()) },
                Scopes = new List<string> { "merchant", "campaign", "notification", "notificationhub", "favorite" }
            },
            new("notificationhub", "Notification Hub")
            {
                ApiSecrets = { new Secret("apisecret".Sha256()) },
                Scopes = new List<string> { "notificationhub" }
            },
            new("subscriptionapi", "Subscription API") 
            {
                ApiSecrets = { new Secret("apisecret".Sha256()) },
                Scopes = new List<string> { "subscription" }
            },
            new("managementapi", "Management API"),
            new("locationapi", "Location API"),
            new("searchapi", "Search API")
        };

    public static IEnumerable<Client> Clients(IConfiguration configuration)
    {
        var jokerWebAppUrl = configuration.GetValue<string>("JokerWebAppUrl");
        var jokerBackOfficeUrl = configuration.GetValue<string>("JokerBackOfficeUrl");
        return new Client[]
        {
            new()
            {
                AccessTokenType = AccessTokenType.Reference,
                AccessTokenLifetime = 3600,
                AllowOfflineAccess = true,
                UpdateAccessTokenClaimsOnRefresh = true,
                ClientName = "Joker Store Front",
                ClientId = "joker.store.front",
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
                    IdentityServerConstants.LocalApi.ScopeName,
                    "roles",
                    "favorite",
                    "merchant",
                    "campaign"
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                }
            },
            new()
            {
                AccessTokenType = AccessTokenType.Reference,
                AccessTokenLifetime = 3600,
                AllowOfflineAccess = true,
                UpdateAccessTokenClaimsOnRefresh = true,
                ClientName = "Joker Back Office",
                ClientId = "joker.back.office",
                AllowedGrantTypes = GrantTypes.Code,
                RequireConsent = false,
                RequirePkce = true,
                RedirectUris = new List<string>()
                {
                    $"{jokerBackOfficeUrl}/signin-oidc",
                },
                PostLogoutRedirectUris = new List<string>()
                {
                    $"{jokerBackOfficeUrl}/signout-callback-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "organization",
                    "roles",
                    "merchant",
                    "campaign",
                    "notification",
                    "notificationhub"
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                }
            }
        };
    }
}