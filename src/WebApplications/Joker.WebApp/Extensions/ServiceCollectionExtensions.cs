using System;
using System.Threading.Tasks;
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
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = configuration.GetValue<string>("IdentityUrl");
                    options.MetadataAddress = $"{configuration.GetValue<string>("IdentityInternalUrl")}/.well-known/openid-configuration";
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.ClientId = "joker.web.app";
                    options.ClientSecret = "secret";
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.Scope.Add("roles");
                    options.Scope.Add("offline_access");
                    options.Scope.Add("merchantapi");
                    options.Scope.Add("campaignapi");
                    options.Scope.Add("aggregatorapi");
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
                    options.Events.OnRedirectToIdentityProvider = context =>
                    {
                        // Intercept the redirection so the browser navigates to the right URL in your host
                        context.ProtocolMessage.IssuerAddress = $"{configuration.GetValue<string>("IdentityUrl")}/connect/authorize";
                        return Task.CompletedTask;
                    };
                    
                    options.Events.OnRedirectToIdentityProviderForSignOut = context =>
                    {
                        // Intercept the redirection so the browser navigates to the right URL in your host
                        context.ProtocolMessage.IssuerAddress = $"{configuration.GetValue<string>("IdentityUrl")}/connect/endsession";
                        return Task.CompletedTask;
                    };
                    // options.Events.OnRedirectToIdentityProviderForSignOut 
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
            return services;
        }
    }
}