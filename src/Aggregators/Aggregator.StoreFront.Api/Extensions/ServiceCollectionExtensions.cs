using Aggregator.StoreFront.Api.Interceptors;
using Aggregator.StoreFront.Api.Services.BaseGrpc;
using Aggregator.StoreFront.Api.Services.Campaign;
using Aggregator.StoreFront.Api.Services.Favorite;
using Aggregator.StoreFront.Api.Services.Identity;
using Aggregator.StoreFront.Api.Services.Management;
using Aggregator.StoreFront.Api.Services.Merchant;
using Aggregator.StoreFront.Api.Services.Store;
using Campaign.Api.Grpc;
using Favorite.Api.Grpc;
using IdentityServer4.AccessTokenValidation;
using Joker.Consul;
using Management.Api.Grpc;
using Merchant.Api.Grpc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Aggregator.StoreFront.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IBaseGrpcProvider, BaseGrpcProvider>();
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<ICampaignService, CampaignService>();
        services.AddScoped<IFavoriteService, FavoriteService>();
        services.AddScoped<IManagementService, ManagementService>();
        services.AddScoped<IMerchantService, MerchantService>();
        
        services.AddTransient<GrpcExceptionInterceptor>();

        services.AddGrpcClient<ManagementApiGrpcService.ManagementApiGrpcServiceClient>(x =>
                x.Address = new Uri(configuration["serviceUrls:management"]))
            .AddInterceptor<GrpcExceptionInterceptor>();
        
        services.AddGrpcClient<CampaignApiGrpcService.CampaignApiGrpcServiceClient>(x =>
                x.Address = new Uri(configuration["serviceUrls:campaign"]))
            .AddInterceptor<GrpcExceptionInterceptor>();

        services.AddGrpcClient<MerchantApiGrpcService.MerchantApiGrpcServiceClient>(x =>
                x.Address = new Uri(configuration["serviceUrls:merchant"]))
            .AddInterceptor<GrpcExceptionInterceptor>();

        services.AddGrpcClient<FavoriteApiGrpcService.FavoriteApiGrpcServiceClient>(x =>
                x.Address = new Uri(configuration["serviceUrls:favorite"]))
            .AddInterceptor<GrpcExceptionInterceptor>();

        return services;
    }

    public static IServiceCollection AddJokerAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = configuration["urls:identityapi"];
                options.ApiName = "aggregatorstorefrontapi";
                options.ApiSecret = "apisecret";
                options.SupportedTokens = SupportedTokens.Reference;
                options.RequireHttpsMetadata = false;
            });

        return services;
    }

    public static IServiceCollection AddJokerConsul(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConsulServices(x => configuration.GetSection("ServiceDiscovery").Bind(x));
        return services;
    }

    public static IServiceCollection AddJokerAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ScopePolicy", builder =>
            {
                builder.RequireAuthenticatedUser();
            });
        });

        return services;
    }

    public static IServiceCollection AddJokerOpenTelemetry(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenTelemetryTracing(
            (builder) => builder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddGrpcClientInstrumentation()
                .AddJaegerExporter(j =>
                {
                    j.AgentHost = configuration["jaeger:host"];
                    j.AgentPort = int.Parse(configuration["jaeger:port"]);
                })
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("AggregatorStoreFrontApi"))
        );

        return services;
    }
    
    public static IServiceCollection AddJokerIdentityApiClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient("IdentityApi", client =>
        {
            client.BaseAddress = new Uri(configuration["urls:identityapi"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        }).AddPolicyHandler(PolicyExtensions.GetCircuitBreakerPolicy());

        services.AddScoped<IIdentityService, IdentityService>();
        
        return services;
    }
}