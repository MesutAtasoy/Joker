using System;
using Aggregator.Api.Interceptors;
using Aggregator.Api.Services.Campaign;
using Aggregator.Api.Services.Location;
using Aggregator.Api.Services.Management;
using Aggregator.Api.Services.Merchant;
using Aggregator.Api.Services.Store;
using Campaign.Api.Grpc;
using Location.Api.Grpc;
using Management.Api.Grpc;
using Merchant.Api.Grpc;
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
    }
}