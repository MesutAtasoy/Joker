using System;
using System.Threading.Tasks;
using Aggregator.Api.Interceptors;
using Aggregator.Api.Services.Campaign;
using Aggregator.Api.Services.Favorite;
using Aggregator.Api.Services.Location;
using Aggregator.Api.Services.Management;
using Aggregator.Api.Services.Merchant;
using Aggregator.Api.Services.Store;
using Campaign.Api.Grpc;
using Favorite.Api.Grpc;
using IdentityServer4.AccessTokenValidation;
using Joker.Consul;
using Location.Api.Grpc;
using Management.Api.Grpc;
using Merchant.Api.Grpc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMerchantService, MerchantService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IManagementService, ManagementService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            
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
            
            services.AddGrpcClient<FavoriteApiGrpcService.FavoriteApiGrpcServiceClient>(x =>
                    x.Address = new Uri(configuration["serviceUrls:favorite"]))
                .AddInterceptor<GrpcExceptionInterceptor>();
            
            return services;
        }

        public static IServiceCollection AddJokerAuthentication(this IServiceCollection services, IConfiguration configuration)
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
            services.RegisterConsulServices(x => configuration.GetSection("ServiceDiscovery").Bind(x));
            return services;
        }
        
        public static IServiceCollection AddJokerAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ScopePolicy", builder =>
                {
                    builder.RequireAuthenticatedUser();
                    builder.RequireScope("campaign", "merchant");
                    builder.RequireRole("FreeUser","PaidUser");
                });   
            });

            return services;
        }    
    }
}