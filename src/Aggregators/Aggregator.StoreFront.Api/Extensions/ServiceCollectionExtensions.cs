using Aggregator.StoreFront.Api.Interceptors;
using Aggregator.StoreFront.Api.Services.Campaign;
using Aggregator.StoreFront.Api.Services.Favorite;
using Aggregator.StoreFront.Api.Services.Store;
using Campaign.Api.Grpc;
using Favorite.Api.Grpc;
using IdentityServer4.AccessTokenValidation;
using Joker.Consul;
using Merchant.Api.Grpc;
using Microsoft.AspNetCore.Authorization;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Aggregator.StoreFront.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<ICampaignService, CampaignService>();
        services.AddScoped<IFavoriteService, FavoriteService>();

        services.AddTransient<GrpcExceptionInterceptor>();

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
                builder.RequireScope("favorite.read","favorite.create", "merchant.read", "campaign.read");
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
}