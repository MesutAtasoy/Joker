using Aggregator.Api.Interceptors;
using Aggregator.Api.Services.BaseGrpc;
using Aggregator.Api.Services.Campaign;
using Aggregator.Api.Services.Location;
using Aggregator.Api.Services.Management;
using Aggregator.Api.Services.Merchant;
using Aggregator.Api.Services.Store;
using Campaign.Api.Grpc;
using IdentityServer4.AccessTokenValidation;
using Joker.Consul;
using Location.Api.Grpc;
using Management.Api.Grpc;
using Merchant.Api.Grpc;
using Microsoft.AspNetCore.Authorization;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Aggregator.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IBaseGrpcProvider, BaseGrpcProvider>();
        services.AddScoped<IMerchantService, MerchantService>();
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<ICampaignService, CampaignService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IManagementService, ManagementService>();

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

        services.AddGrpcClient<LocationApiGrpcService.LocationApiGrpcServiceClient>(x =>
                x.Address = new Uri(configuration["serviceUrls:location"]))
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
                options.ApiName = "aggregatorapi";
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
                builder.RequireRole("Admin","PaidUser");
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
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("AggregatorApi"))
        );

        return services;
    }
}