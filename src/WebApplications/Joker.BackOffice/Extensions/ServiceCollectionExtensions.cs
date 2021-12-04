using IdentityModel;
using Joker.BackOffice.HttpHandlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Joker.BackOffice.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJokerAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<BearerTokenHandler>();

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.AccessDeniedPath = "/Authorization/AccessDenied";
                options.LoginPath = "/Account/Login";
            })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration.GetValue<string>("IdentityUrl");
                options.MetadataAddress = $"{configuration.GetValue<string>("IdentityInternalUrl")}/.well-known/openid-configuration";
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.ClientId = "joker.back.office";
                options.ClientSecret = "secret";
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.Scope.Add("roles");
                options.Scope.Add("merchant.create");
                options.Scope.Add("merchant.read");
                options.Scope.Add("merchant.delete");
                options.Scope.Add("campaign.create");
                options.Scope.Add("campaign.read");
                options.Scope.Add("campaign.delete");
                options.Scope.Add("IdentityServerApi");
                options.ClaimActions.DeleteClaim("sid");
                options.ClaimActions.DeleteClaim("idp");
                options.ClaimActions.DeleteClaim("s_hash");
                options.ClaimActions.DeleteClaim("auth_time");
                options.ClaimActions.MapUniqueJsonKey("role", "role");
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.GivenName,
                    RoleClaimType = JwtClaimTypes.Role
                };
                options.Events.OnRedirectToIdentityProvider = context => context.RedirectToIdentityProvider(configuration.GetValue<string>("IdentityUrl"));
                options.Events.OnRedirectToIdentityProviderForSignOut = context => context.RedirectToIdentityProviderForSignOut(configuration.GetValue<string>("IdentityUrl"));
                options.Events.OnUserInformationReceived = context => context.MapRoles(); 
                options.RequireHttpsMetadata = false;
            });
        return services;
    }
}