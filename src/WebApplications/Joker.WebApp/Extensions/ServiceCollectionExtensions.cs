using IdentityModel;
using Joker.CAP;
using Joker.WebApp.Events;
using Joker.WebApp.HttpHandlers;
using Joker.WebApp.Services;
using Joker.WebApp.Services.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Joker.WebApp.Extensions;

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
                options.ClientId = "joker.store.front";
                options.ClientSecret = "secret";
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.Scope.Add("roles");
                options.Scope.Add("offline_access");
                options.Scope.Add("favorite");
                options.Scope.Add("merchant");
                options.Scope.Add("campaign");
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

    public static IServiceCollection AddJokerGatewayApiClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient("GatewayApi", client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("GatewayUrl"));
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<BearerTokenHandler>()
            .AddPolicyHandler(PolicyExtensions.GetCircuitBreakerPolicy());

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
        }).AddPolicyHandler(PolicyExtensions.GetCircuitBreakerPolicy());

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<ISearchService, SearchService>();
        services.AddScoped<IFavoriteService, FavoriteService>();
        services.AddScoped<IManagementService, ManagementApiService>();
        services.AddScoped<IMerchantService, MerchantService>();
        return services;
    }

    public static IServiceCollection AddUserServices(this IServiceCollection services)
    {
        return services.AddTransient<IUserService, UserService>();
    }
        
    public static IServiceCollection AddJokerEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJokerCAP(capOptions =>
        {
            capOptions.UseRabbitMQ(x =>
            {
                x.Password = configuration["rabbitMQSettings:password"];
                x.UserName = configuration["rabbitMQSettings:username"];
                x.HostName = configuration["rabbitMQSettings:host"];
                x.Port = int.Parse(configuration["rabbitMQSettings:port"]);
            });

            capOptions.UseMongoDB(opt => // Persistence
            {
                opt.DatabaseConnection = configuration["mongo:ConnectionString"];
                opt.DatabaseName = configuration["mongo:DefaultDatabaseName"] + "-eventHistories";
                opt.PublishedCollection = "PublishedEvents";
                opt.ReceivedCollection = "ReceivedEvents";
            });

            capOptions.FailedRetryCount = 3;
            capOptions.FailedRetryInterval = 60;
        });
            
        services.RegisterCAPEventHandlers(typeof(CampaignCreatedNotificationEvent));

        return services;
    }
        
    public static IServiceCollection AddJokerOpenTelemetry(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenTelemetryTracing(
            (builder) => builder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddJaegerExporter(j =>
                {
                    j.AgentHost = configuration["jaeger:host"];
                    j.AgentPort = int.Parse(configuration["jaeger:port"]);
                })
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("JokerWebApp"))
        );

        return services;
    }
}