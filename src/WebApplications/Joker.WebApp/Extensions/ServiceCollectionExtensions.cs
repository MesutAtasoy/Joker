using System;
using IdentityModel;
using Joker.WebApp.HttpHandlers;
using Joker.WebApp.Services;
using Joker.WebApp.Services.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace Joker.WebApp.Extensions
{
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
                    options.MetadataAddress =
                        $"{configuration.GetValue<string>("IdentityInternalUrl")}/.well-known/openid-configuration";
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.ClientId = "joker.web.app";
                    options.ClientSecret = "secret";
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.Scope.Add("roles");
                    options.Scope.Add("offline_access");
                    options.Scope.Add("merchant");
                    options.Scope.Add("campaign");
                    options.Scope.Add("subscription");
                    options.Scope.Add("organization");
                    options.ClaimActions.DeleteClaim("sid");
                    options.ClaimActions.DeleteClaim("idp");
                    options.ClaimActions.DeleteClaim("s_hash");
                    options.ClaimActions.DeleteClaim("auth_time");
                    options.ClaimActions.MapUniqueJsonKey("role", "role");
                    options.ClaimActions.MapUniqueJsonKey("organizationId", "organizationId");
                    options.ClaimActions.MapUniqueJsonKey("organizationName", "organizationName");
                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = JwtClaimTypes.GivenName,
                        RoleClaimType = JwtClaimTypes.Role
                    };
                    options.Events.OnRedirectToIdentityProvider = context => 
                        context.RedirectToIdentityProvider(configuration.GetValue<string>("IdentityUrl"));
                    options.Events.OnRedirectToIdentityProviderForSignOut = context => 
                        context.RedirectToIdentityProviderForSignOut(configuration.GetValue<string>("IdentityUrl"));
                    options.Events.OnUserInformationReceived = context => 
                        context.MapRoles(); 
                    options.RequireHttpsMetadata = false;
                });
            return services;
        }

        public static IServiceCollection AddJokerGatewayApiClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpClient("GatewayApi", client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("GatewayUrl"));
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<BearerTokenHandler>();

            return services;
        }

        public static IServiceCollection AddJokerIdentityApiClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpClient("IdentityApi", client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("IdentityInternalUrl"));
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });

            return services;
        }

        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IManagementApiService, ManagementApiService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IMerchantService, MerchantService>();
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ILocationService, LocationService>();
            return services;
        }

        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            return services.AddTransient<IUserService, UserService>();
        }
    }
}